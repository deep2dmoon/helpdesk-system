using helpdesk.Application;
using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.Services;

namespace helpdesk.Controller.Auth;

public static class AuthEndpoint
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this WebApplication app)
    {
        RouteGroupBuilder AuthGroup = app.MapGroup("/api/auth").WithParameterValidation();

        AuthGroup.MapPost("/login", (AuthService authService, UserDTO userDTO) =>
        {
            User? user = authService.ValidateUser(userDTO);
            object account = new { Name = user?.FullName, email = user?.Email, role = user?.Role };

            return user is not null ? Results.Ok(new
            {
                response = "Success",
                account,
                token = authService.GenerateJwtToken(user),
            }) : Results.BadRequest(new { response = "Invalid user credentials" });
        });


        AuthGroup.MapPost("/signup", (AccountMaker accountMaker, NewAccountDTO accountDTO) =>
        {
            object response = accountMaker.MakeCustomer(accountDTO);
            return Results.Ok(response);
        });
        return AuthGroup;
    }
}