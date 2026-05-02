namespace helpdesk.Entities;

public class Message
{
    public int MessageID { get; set; }
    public required string Sender { get; set; }
    public required string MessageData { get; set; }
}