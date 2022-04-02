using GameCaro.Models;
using Microsoft.AspNetCore.SignalR;

namespace GameCaro.Hubs
{
    public class CaroHub:Hub
    {
        public async Task SendMessage(string nameId, string user, string message, string room, bool join)
        {
            if (join)
            {
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessage", nameId, user, " joined to " + room).ConfigureAwait(true);

            }
            else
            {
                await Clients.Group(room).SendAsync("ReceiveMessage", nameId, user, message).ConfigureAwait(true);

            }
        }

        public async Task SendMessageGroups(string user, string message, string room, bool join)
        {
            if (join)
            {
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessageGroups", user, " joined to groups " + room).ConfigureAwait(true);

            }
            else
            {
                await Clients.Group(room).SendAsync("ReceiveMessageGroups", user, message).ConfigureAwait(true);

            }
        }
        public Task JoinRoom(string roomName)
        {
            
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public override Task OnConnectedAsync()
        {
            ConnectedUser.Ids.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUser.Ids.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
