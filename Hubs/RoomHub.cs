using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PokerApi.Hubs;

[Authorize]
public class RoomHub : Hub
{
    // This hub can be used for real-time communication related to rooms, such as notifications when a room is created, updated, or deleted.

    //Client peut rejoindre le groupe d'une room spécifique
    public async Task JoinRoomGroup(int roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Room-{roomId}");
    }

    public async Task LeaveRoomGroup(int roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Room-{roomId}");
    }
}