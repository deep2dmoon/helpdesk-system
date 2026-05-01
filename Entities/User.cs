namespace helpdesk.Entities;

public class User
{
    public int UserId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Role { get; set; }
    public required string Password { get; set; }
}