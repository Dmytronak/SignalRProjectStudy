using Microsoft.AspNetCore.Mvc;
using SignalRProject.BusinessLogic.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.Web.Components
{
    public class UserCaseOnNavViewComponent : ViewComponent
    {
        private readonly IAuthService _authService;

        public UserCaseOnNavViewComponent(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext?.User?.Claims?.ToList()?.FirstOrDefault()?.Value;
            var model = await _authService.GetbyId(userId);
            return View(model);
        }
    }
}