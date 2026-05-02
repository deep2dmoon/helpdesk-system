using helpdesk.Entities;

namespace helpdesk.DTOs;

public record class TicketDTOResults(
    string ID,
    string Issuer,
    string Category,
    string Description,
    string Attendant,
    List<Message> Conversations,
    TicketStatus Status,
    List<FileMetaData>? Files
);