using System.ComponentModel.DataAnnotations;

namespace helpdesk.DTOs;

public record class MessageDTO(
[Required][StringLength(20)] string TicketID,
[Required][StringLength(20)] string Sender,
[Required][StringLength(300)] string Response
);