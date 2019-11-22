using Microsoft.AspNetCore.Http;

namespace SignalRProject.ViewModels.ChatViewModel
{
    public class CreateRoomChatView
    {
        public string Name { get; set; }

        public IFormFile Photo { get; set; }

    }
}
