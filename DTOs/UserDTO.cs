using System.ComponentModel.DataAnnotations;

namespace helpdesk.DTOs;

public record class UserDTO(
    [Required][StringLength(20)] string Email,
    [Required][StringLength(20)] string Password
);