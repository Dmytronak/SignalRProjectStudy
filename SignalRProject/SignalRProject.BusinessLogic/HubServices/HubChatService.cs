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

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage("Notify", $"Greetings  {Context.UserIdentifier}"); //
            await base.OnConnectedAsync();
        }
    }
}
