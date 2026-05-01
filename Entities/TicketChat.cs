namespace helpdesk.Entities;

public class TicketChat
{
    public string? ID { get; set; }
    public List<Message> Messages { get; set; } = [];
}