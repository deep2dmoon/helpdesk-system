using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.Helper;
using helpdesk.infrastructure;
using helpdesk.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace helpdesk.Application;

public class Helpdesk(DatabaseContext _context, IHubContext<NotificationService> notification_, ILogger<Helpdesk> logger_)
{
    private readonly ILogger<Helpdesk> logger = logger_;


    private readonly IHubContext<NotificationService> notifier = notification_;
    private readonly DatabaseContext context = _context;
    public async Task<object> RaiseTicket(TicketDTO ticketDTO)
    {
        try
        {
            string TicketID = UniqueID.GenerateUID();
            Ticket NewTicket = new()
            {
                ID = TicketID,
                Issuer = ticketDTO.Issuer,
                Category = context.TicketCategories.FirstOrDefault(c => c.ID == ticketDTO.CategoryID)!,
                Description = ticketDTO.Description
            };
            context.Tickets.Add(NewTicket);
            await context.SaveChangesAsync();
            await notifier.Clients.Group("Admins").SendAsync("NewTicket", NewTicket);
            logger.LogInformation("New Ticket {ticketID} raised {by}", NewTicket.ID, NewTicket.Issuer);
            return new { response = "New Ticket Raised", NewTicket.ID };
        }
        catch (System.Exception)
        {
            logger.LogError("Error raising new ticket");
            throw;
        }
    }


    public async Task<object> OpenTicket(string Attendant, string _ticketID)
    {
        try
        {
            Ticket? ticket = context.Tickets.FirstOrDefault(t => t.ID == _ticketID);
            if (ticket is null)
            {
                return new { response = "Ticket not found/closed" };
            }
            ticket.Attendant = Attendant;
            ticket.Status = TicketStatus.Open;
            context.SaveChanges();

            User? Issuer = context.Users.FirstOrDefault(u => u.Email == ticket.Issuer);
            await notifier.Clients.User(Issuer!.UserId).SendAsync("TicketOpened", ticket);
            logger.LogInformation("Ticket opened {ticket}", ticket);
            return new { response = $"Ticket {ticket.ID} raised by {ticket.Issuer} now Open" };
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public List<Ticket> GetPendingTickets()
    {
        List<Ticket> PendingTickets = [.. context.Tickets.Include(t => t.Category).Where(ticket => ticket.Status == TicketStatus.Pending)];
        return PendingTickets;
    }

    public List<Ticket> GetOpenedTickets()
    {
        List<Ticket> OpenTickets = [.. context.Tickets.Include(t => t.Conversation).Include(t => t.Category).Include(t => t.FileMetaData).Where(ticket => ticket.Status == TicketStatus.Open)];
        return OpenTickets;
    }
    public async Task<string> CloseTicket(string ticketID)
    {
        try
        {
            Ticket? ticket = await context.Tickets.FirstOrDefaultAsync(t => t.ID == ticketID);
            ticket!.Status = TicketStatus.Closed;
            await context.SaveChangesAsync();
            logger.LogInformation("ticket {ticketID} closed", ticketID);
            return ticket.ID!;
        }
        catch (System.Exception)
        {
            logger.LogError("Error closing the ticket");
            throw;
        }
    }

    public async Task AddFile(string ticketID, FileMetaData file)
    {
        Ticket? ticket = await context.Tickets.FirstOrDefaultAsync(t => t.ID == ticketID);
        ticket!.FileMetaData!.Add(file);
        await context.SaveChangesAsync();
    }


    public List<Ticket> GetAllTickets()
    {
        return context.Tickets.Include(t => t.Category).Include(t => t.Conversation).Include(t => t.FileMetaData).ToList();
    }
}