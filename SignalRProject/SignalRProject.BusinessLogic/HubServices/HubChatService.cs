using Microsoft.AspNetCore.SignalR;
using SignalRProject.BusinessLogic.HubServices.Interfaces;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.HubServices
{
    public class HubChatService : Hub<IHubChatService>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.ReceiveMessage(message);
        }
    }
}
