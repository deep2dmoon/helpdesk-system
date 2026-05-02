using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.Helper;
using helpdesk.infrastructure;

namespace helpdesk.Application;

public class AccountMaker(DatabaseContext _context)
{
    private readonly DatabaseContext context = _context;
    public object MakeCustomer(NewAccountDTO newAccountDTO)
    {
        User NewAccount = new()
        {
            UserId = UniqueID.GenerateUID(),
            Email = newAccountDTO.Email,
            FullName = newAccountDTO.FullName,
            Password = newAccountDTO.Password,
            Role = "User"
        };

        User? user = context.Users.FirstOrDefault(account => account.Email == newAccountDTO.Email);
        if (user != null)
            return new { response = "Email already exist, sign in" };
        context.Users.Add(NewAccount); context.SaveChanges();
        return new { response = "Account successfully created", NewAccount };
    }
}