using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.ViewModels.ChatViewModel;
using System.Threading.Tasks;

namespace SignalRProject.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _chatService.GetAllRooms();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomChatView model)
        {
            await _chatService.CreateRoom(model);
            return Ok();
        }
    }
}
