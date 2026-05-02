using System.Text.Json.Serialization;
using helpdesk.DTOs;

namespace helpdesk.Entities;

public class Ticket
{

    public string? ID { get; set; }
    public required string Issuer { get; set; }
    public string? Attendant { get; set; }
    public List<Message> Conversation { get; set; } = [];
    public TicketStatus Status { get; set; } = TicketStatus.Pending;
    public required TicketCategory Category { get; set; }
    public required string Description { get; set; }
    public List<FileMetaData>? FileMetaData { get; set; } = [];

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketStatus
{
    Pending, Open, Closed
}