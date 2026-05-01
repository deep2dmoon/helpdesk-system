using helpdesk.Application;
using helpdesk.Entities;

namespace helpdesk.Controller.HelpdeskEndpoint;

public static class HelpdeskEndpoint
{
    public static RouteGroupBuilder MapHelpdeskEndpoint(this WebApplication app)
    {
        RouteGroupBuilder HelpdeskGroup = app.MapGroup("/api/helpdesk").RequireAuthorization(policy => policy.RequireRole("Admin"));

        HelpdeskGroup.MapGet("/all/tickets", (Helpdesk helpdesk) =>
        {
            List<Ticket> tickets = helpdesk.GetOpenedTickets();
            return Results.Ok(tickets);
        });

        return HelpdeskGroup;
    }
}