using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace helpdesk.Services;

public class AuthService(DatabaseContext _context)
{
    private readonly DatabaseContext context = _context;
    public User? ValidateUser(UserDTO userDTO)
    {
        User? user = context.Users.FirstOrDefault(user => user.Email == userDTO.Email);
        if (user is not null)
        {
            if (user.Password == userDTO.Password)
            {
                return new User
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FullName = user.FullName,
                    Password = user.Password,
                    Role = user.Role
                };
            }
        }
        return null;
    }
    public string GenerateJwtToken(User user)
    {
        var claims = new[] { new Claim(ClaimTypes.Name, user.FullName), new Claim(ClaimTypes.Role, user.Role!), new Claim(ClaimTypes.GivenName, user.Password) };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("random-strings-for-generating-symmetical-key"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(1), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}