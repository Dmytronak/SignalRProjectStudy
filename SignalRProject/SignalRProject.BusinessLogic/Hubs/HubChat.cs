using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRProject.BusinessLogic.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HubChat : Hub
    {
        #region Properties

        private readonly IChatService _chatService;
        private readonly DateTime _dateTime;
        #endregion Properties

        #region Constructor

        public HubChat(IChatService chatService)
        {
            _chatService = chatService;
            _dateTime = DateTime.UtcNow;
        }

        #endregion Constructor

        #region OnConnectedAsync/OnDisconnectedAsync

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", $"{Context.UserIdentifier} join to converstion");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected",$"{Context.UserIdentifier} has left converstion");
            await base.OnDisconnectedAsync(ex);
        }

        #endregion OnConnectedAsync/OnDisconnectedAsync

        #region RoomActions

        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.UserIdentifier, Context.User.Identity.Name, message, $"{_dateTime}");
        }

        public async Task SendMessageToRoom(string group, string roomId,string message)
        {
            await _chatService.CreateMessage(message, Guid.Parse(roomId), Context.User.Identity.Name);
            await Clients.Group(group).SendAsync("ReceiveMessage", Context.UserIdentifier, Context.User.Identity.Name, message, $"{_dateTime}");
        }

        public async Task SendMessageToCaller(string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToUser(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinRoom(string roomId, string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("UserConnected", $"{Context.UserIdentifier} join to converstion");
            await _chatService.SetUserCurrentRoomByUserId(Context.User.Identity.Name, Guid.Parse(roomId));

            var room = await _chatService.GetAllMessagesByRoomId(Guid.Parse(roomId));

            foreach (var r in room.Messages)
            {
                await Clients.Group(group).SendAsync("ReceiveMessage", Context.UserIdentifier, Context.User.Identity.Name, r.Text, $"{r.CreationAt}");
            }
            
        }

        public async Task LeaveRoom(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("UserConnected", $"{Context.UserIdentifier} has left converstion");
        }


        #endregion RoomActions

    }
}
