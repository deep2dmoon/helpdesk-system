using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.Helper;
using helpdesk.infrastructure;
namespace helpdesk.Application;

public class Helpdesk(DatabaseContext _context)
{
    private readonly DatabaseContext context = _context;
    public object RaiseTicket(TicketDTO ticketDTO)
    {
        string TicketID = UniqueID.GenerateUID();
        Ticket NewTicket = new()
        {
            ID = TicketID,
            Issuer = ticketDTO.Issuer,
            Category = context.TicketCategories.Find(ticketDTO.CategoryID)!,
            Description = ticketDTO.Description
        };
        context.Tickets.Add(NewTicket);
        context.SaveChangesAsync();
        return new { response = "New Ticket Raised", NewTicket.ID };
    }
    public object OpenTicket(string Admin, string _ticketID)
    {
        Ticket? ticket = context.Tickets.Find(_ticketID);
        if (ticket is null)
        {
            return new { response = "Ticket not found or closed" };
        }
        ticket.Attendant = Admin;
        ticket.Status = TicketStatus.Open;
        context.SaveChanges();
        return new { response = $"Ticket {ticket.ID} raised by {ticket.Issuer} now Open" };
    }

    public List<Ticket> GetPendingTickets()
    {
        List<Ticket> PendingTickets = [.. context.Tickets.Where(ticket => ticket.Status == TicketStatus.Pending)];
        return PendingTickets;
    }

    public List<Ticket> GetOpenedTickets()
    {
        List<Ticket> OpenTickets = [.. context.Tickets.Where(ticket => ticket.Status == TicketStatus.Open)];
        return OpenTickets;
    }
    public void CloseTicket() { }
}