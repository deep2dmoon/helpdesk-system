using System.ComponentModel.DataAnnotations;

namespace helpdesk.DTOs;

public record class TicketDTO(
    [Required][StringLength(30)] string Issuer,
    [Required] int CategoryID,
    [Required][StringLength(300)] string Description
);