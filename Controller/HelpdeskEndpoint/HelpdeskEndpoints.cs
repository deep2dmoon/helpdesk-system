using helpdesk.Application;
using helpdesk.DTOs;
using helpdesk.Entities;
using helpdesk.Helper;
using helpdesk.infrastructure;

namespace helpdesk.Controller.HelpdeskEndpoint;

public static class HelpdeskEndpoint
{
    public static RouteGroupBuilder MapHelpdeskEndpoint(this WebApplication app)
    {
        RouteGroupBuilder HelpdeskGroup = app.MapGroup("/api/helpdesk");

        HelpdeskGroup.MapGet("/pending/tickets", async (Helpdesk helpdesk) =>
        {
            List<Ticket> tickets = helpdesk.GetPendingTickets();
            return Results.Ok(tickets.Select(t => new TicketDTOResults(t.ID!, t.Issuer, t.Category.Title, t.Description, t.Attendant!, t.Conversation, t.Status, t.FileMetaData)));
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));


        HelpdeskGroup.MapGet("/open/tickets", async (Helpdesk helpdesk) =>
              {
                  List<Ticket> tickets = helpdesk.GetOpenedTickets();
                  return Results.Ok(tickets.Select(t => new TicketDTOResults(t.ID!, t.Issuer, t.Category.Title, t.Description, t.Attendant!, t.Conversation, t.Status, t.FileMetaData)));
              }).RequireAuthorization(policy => policy.RequireRole("Admin"));


        HelpdeskGroup.MapPatch("/close/ticket/{ticketID}", async (Helpdesk helpdesk, string ticketID) =>
        {
            await helpdesk.CloseTicket(ticketID);
            return Results.Ok($"Ticket {ticketID} successfully closed ");
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));


        HelpdeskGroup.MapPost("/new/ticket", async (Helpdesk helpdesk, TicketDTO ticket) =>
        {
            object response = await helpdesk.RaiseTicket(ticket);
            return Results.Ok(response);
        }).RequireAuthorization(policy => policy.RequireRole("User"));

        HelpdeskGroup.MapGet("/category", async (DatabaseContext context) => context.TicketCategories);


        HelpdeskGroup.MapPost("/send/message", async (Messenger messenger, MessageDTO messageDTO) =>
        {
            object response = messenger.Send(messageDTO);
            return Results.Ok(response);
        });

        HelpdeskGroup.MapPost("/admin/{admin}/open/ticket/{ticketID}", async (string admin, string ticketID, Helpdesk helpdesk) =>
        {
            object response = await helpdesk.OpenTicket(admin, ticketID);
            return Results.Ok(response);
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));


        HelpdeskGroup.MapPost("/ticket/{ticketID}/file/upload", async (IFormFile file, FileUploader uploader, string ticketID, Helpdesk document) =>
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest(new { response = "No file received, select file and try again" });

            if (file.Length > 5_000)
                return Results.BadRequest("Supported file size is 5MB");

            FileMetaData fileMetaData = await uploader.Upload(file);
            await document.AddFile(ticketID, fileMetaData);

            return Results.Ok($"File uploaded successfully {fileMetaData}");
        }).DisableAntiforgery();



        HelpdeskGroup.MapGet("/guest/all/tickets", async (DatabaseContext context) =>
        {
            return Results.Ok(context.Tickets);
        });

        HelpdeskGroup.MapGet("/admin/all/tickets", (Helpdesk helpdesk) =>
        {
            List<Ticket> tickets = helpdesk.GetAllTickets();
            return Results.Ok(tickets.Select(t => new TicketDTOResults(t.ID!, t.Issuer, t.Category.Title, t.Description, t.Attendant!, t.Conversation, t.Status, t.FileMetaData)));
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        return HelpdeskGroup;
    }

}