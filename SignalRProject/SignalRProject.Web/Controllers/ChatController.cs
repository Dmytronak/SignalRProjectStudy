using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRProject.BusinessLogic.Fabrics.Interfaces;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.ViewModels.ChatViews;
using System;
using System.Threading.Tasks;

namespace SignalRProject.Web.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService, IChatMenuItemsFabric chatMenuItemsFabric) : base(chatMenuItemsFabric)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

 
        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomChatView model)
        {
            await _chatService.CreateRoom(model,UserId);
            return Redirect("Index");
        }
    }
}
