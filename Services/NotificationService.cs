using System.Security.Claims;
using helpdesk.infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace helpdesk.Services;

public class NotificationService(DatabaseContext database_) : Hub
{
    DatabaseContext database = database_;
    private HubConnectionStore? HubConnectionContexts;
    public const string Admins = "Admins";
    public const string Others = "Others";

    public override async Task OnConnectedAsync()
    {
        var connectedAccountRole = Context.User!.FindFirst(ClaimTypes.Role)?.Value;
        if (connectedAccountRole == "Admin")
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Admins);
        }
        await Groups.AddToGroupAsync(connectedAccountRole!, Others);
        await base.OnConnectedAsync();
    }

}