using SignalRProject.ViewModels.ChatViews;
using System;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Services.Interfaces
{
    public interface IChatService
    {
        Task<GetAllRoomsChatView> GetAllRooms();
        Task<GetAllRoomsChatView> GetByUserId(string userId);
        Task CreateRoom(CreateRoomChatView model, string userId);
        Task<GetRoomChatView> GetRoomById(Guid roomId);
        Task<GetAllMessagesChatView> GetAllMessagesByRoomId(Guid roomId);
        Task<GetUserCurrentRoomChatView> GetUserCurrentRoomByUserId(string userId);
        Task SetUserCurrentRoomByUserId(string userId, Guid roomId);
        Task CreateMessage(string messageText, Guid roomId, string userId);
    }
}
