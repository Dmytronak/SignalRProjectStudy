using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SignalRProject.BusinessLogic.Fabrics.Interfaces;
using SignalRProject.ViewModels.MenuViewModel;
using System.Linq;

namespace SignalRProject.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IChatMenuItemsFabric _chatMenuItemsFabric;

        public BaseController(IChatMenuItemsFabric chatMenuItemsFabric)
        {
            _chatMenuItemsFabric = chatMenuItemsFabric;
        }

        public string UserId { get { return HttpContext?.User?.Claims?.ToList()?.FirstOrDefault()?.Value ?? string.Empty; } }

        #region Override Methods

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ChatMenuView menu = _chatMenuItemsFabric.BuildChatMenu(UserId);
            ViewData["Chats"] = menu;
        }

        #endregion
      
    }
}
