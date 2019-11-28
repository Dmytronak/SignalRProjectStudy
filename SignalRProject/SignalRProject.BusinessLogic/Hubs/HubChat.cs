using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.ViewModels.ChatViews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HubChat : Hub
    {
        #region Properties

        private readonly IChatService _chatService;

        #endregion Properties

        #region Constructor

        public HubChat(IChatService chatService)
        {
            _chatService = chatService;
        }

        #endregion Constructor

        #region OnConnectedAsync/OnDisconnectedAsync

        public override async Task OnConnectedAsync()
        {
            GetAllRoomsChatView userInRoom = await _chatService.GetByUserId(Context.User.Identity.Name);

            foreach (var room in userInRoom.Rooms)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room.Name);
                await Clients.Group(room.Name).SendAsync("Notify", $"Greetings  {Context.UserIdentifier}");
            }

            //await Clients.All.SendAsync("Notify", $"Greetings  {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.UserIdentifier} has left a conversation");
            await base.OnDisconnectedAsync(exception);
        }

        #endregion OnConnected/OnDisconnected

        #region SendingMessages

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, $"{Context.User.Identity.Name} {message}");
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
