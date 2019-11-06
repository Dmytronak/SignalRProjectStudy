using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.HubServices.Interfaces
{
    public interface IHubChatService 
    {
       public Task ReceiveMessage(string user, string message);
       public Task ReceiveMessage(string message);
    }
}
