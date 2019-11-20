using SignalRProject.ViewModels.ChatViewModel;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Services.Interfaces
{
    public interface IChatService
    {
        Task<GetAllRoomsChatView> GetAllRooms();
        Task CreateRoom(CreateRoomChatView model);
    }
}
