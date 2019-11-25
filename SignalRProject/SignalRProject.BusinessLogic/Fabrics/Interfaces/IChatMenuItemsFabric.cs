using SignalRProject.ViewModels.MenuViewModel;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Fabrics.Interfaces
{
    public interface IChatMenuItemsFabric
    {
        ChatMenuView BuildChatMenu(string userId);
    }
}
