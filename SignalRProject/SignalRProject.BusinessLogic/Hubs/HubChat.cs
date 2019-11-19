using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Hubs
{
    public class HubChat : Hub
    {
        #region Notify

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"Greetings  {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.UserIdentifier} has left a conversation");
            await base.OnDisconnectedAsync(exception);
        }

        #endregion Notify

        #region SendingMessages

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, $"{Context.UserIdentifier} {message}");
        }

        #endregion SendingMessages

        #region RoomActions

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        #endregion RoomActions

    }
}
