using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.ViewModels.ChatViews;
using System;
using System.Collections.Generic;
using System.Linq;
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
            GetUserAndCurrentRoomChatView user = await _chatService.GetUserAndCurrentRoomByUserId(Context.User.Identity.Name);
            GetAllRoomsChatView userRooms = await _chatService.GetByUserId(Context.User.Identity.Name);

            List<string> roomNames = userRooms.Rooms
                .Where(x=>x.Id == user.CurrentRoomId)
                .Select(x => x.Name)
                .ToList();

            GetAllUsersChatView usersInRoom = _chatService.GetAllUsersByRoomId(user.CurrentRoomId);
            await Clients.Groups(roomNames).SendAsync("UserConnected", $"{user.FirstName} {user.LastName} join to conversation", user, usersInRoom);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            GetUserAndCurrentRoomChatView user = await _chatService.GetUserAndCurrentRoomByUserId(Context.User.Identity.Name);
            GetAllRoomsChatView userRooms = await _chatService.GetByUserId(Context.User.Identity.Name);
            List<string> roomNames = userRooms.Rooms
                .Select(x => x.Name)
                .ToList();

            await Clients.Groups(roomNames).SendAsync("UserDisconnected", $"{user.FirstName} {user.LastName} has left conversation", user);
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
            GetCreateMessageChatView messageModel =  await _chatService.CreateMessage(message, Guid.Parse(roomId), Context.User.Identity.Name);
            await Clients.Group(group).SendAsync("ReceiveMessage", messageModel);
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
            await _chatService.SetUserCurrentRoomByUserId(Context.User.Identity.Name, Guid.Parse(roomId));

            GetUserAndCurrentRoomChatView user = await _chatService.GetUserAndCurrentRoomByUserId(Context.User.Identity.Name);
            GetAllMessagesChatView room = await _chatService.GetAllMessagesByRoomId(Guid.Parse(roomId));
            GetAllUsersChatView usersInRoom = _chatService.GetAllUsersByRoomId(user.CurrentRoomId);

            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("UserConnected", $"{user.FirstName} {user.LastName} join to conversation", user, usersInRoom);
            await Clients.Group(group).SendAsync("ReceiveRoomMessages", room);

        }

        public async Task LeaveRoom(string group)
        {
            GetUserAndCurrentRoomChatView user = await _chatService.GetUserAndCurrentRoomByUserId(Context.User.Identity.Name);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("UserDisconnected", $"{user.FirstName} {user.LastName} has left converstion", user);
        }


        #endregion RoomActions

    }
}
