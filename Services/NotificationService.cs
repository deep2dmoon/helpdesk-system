using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace helpdesk.Services;

public class NotificationService : Hub
{
    public const string Admins = "Admins";

    public override async Task OnConnectedAsync()
    {
        var connectedAccountRole = Context.User!.FindFirst(ClaimTypes.Role)?.Value;
        if (connectedAccountRole == "Admin")
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Admins);
        }
        await base.OnConnectedAsync();
    }
}