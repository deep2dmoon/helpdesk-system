using System.ComponentModel.DataAnnotations;

namespace helpdesk.DTOs;

public record class NewAccountDTO(
   [Required][StringLength(30)] string FullName,
   [Required][StringLength(30)] string Email,
   [Required][StringLength(30)] string Password
);