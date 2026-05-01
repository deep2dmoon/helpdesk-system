namespace helpdesk.Entities;

public class Ticket
{

    public string? ID { get; set; }
    public required string Issuer { get; set; }
    public string? Attendant { get; set; }
    public TicketChat Conversation { get; set; } = new TicketChat() { };
    public TicketStatus Status { get; set; } = TicketStatus.Pending;
    public required TicketCategory Category { get; set; }
    public required string Description { get; set; }
}
public enum TicketStatus
{
    Pending, Open, Closed
}