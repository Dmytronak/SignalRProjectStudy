using SignalRProject.ViewModels.ChatViews;
using System;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Services.Interfaces
{
    public interface IChatService
    {
        Task<GetAllRoomsChatView> GetByUserId(string userId);
        Task CreateRoom(CreateRoomChatView model, string userId);
        Task<GetRoomChatView> GetRoomById(Guid roomId);
        Task<GetAllMessagesChatView> GetAllMessagesByRoomId(Guid roomId);
    }
}
