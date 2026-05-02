using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.infrastructure;
using helpdesk.Services;
using Microsoft.AspNetCore.SignalR;

namespace helpdesk.Application;

public class Messenger(DatabaseContext context_, ILogger<Messenger> logger_, IHubContext<NotificationService> notification_)
{

    ILogger<Messenger> logger = logger_;
    IHubContext<NotificationService> notifier = notification_;
    private readonly DatabaseContext context = context_;
    public object Send(MessageDTO messageDTO)
    {
        object response;
        try
        {
            Ticket? ticket = context.Tickets.FirstOrDefault(t => t.ID == messageDTO.TicketID);
            Message message = new() { MessageData = messageDTO.Response, Sender = messageDTO.Sender };
            if (ticket?.Status != TicketStatus.Open)
            {
                return response = new { response = "Ticket not open yet for conversation..." };
            }
             if (ticket?.Status == TicketStatus.Closed)
            {
                return response = new { response = "Ticket closed for further conversation..." };
            }
            
            ticket!.Conversation.Add(message);
            context.SaveChanges();
            logger.LogInformation("Message sent {from} {message}", message.Sender, message.MessageData);


            string[] parties = [ticket?.Attendant!, ticket?.Issuer!];
            for (int i = 0; i < parties.Length; i++)
            {
                string user = parties[i];
                if (user == message.Sender) continue;
                User User = context.Users.FirstOrDefault(u => u.Email == user)!;
                string UID = User.UserId;
                notifier.Clients.User(UID).SendAsync("NewMessage", message.MessageData);
            }
            return response = new { response = "Message sent..." };
        }
        catch (System.Exception)
        {
            logger.LogError("Message sending failed");
            throw;
        }
    }
}