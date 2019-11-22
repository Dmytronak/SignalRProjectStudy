using SignalRProject.ViewModels.ChatViewModel;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Services.Interfaces
{
    public interface IChatService
    {
        Task<GetAllRoomsChatView> GetAllRooms(string userId);
        Task CreateRoom(CreateRoomChatView model, string userId);
    }
}
