using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.ViewModels.AuthViewModel;

namespace SignalRProject.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login(LoginAuthResponseView loginAuthResponse)
        {
            return View(loginAuthResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAuthView model)
        {
            var response = await _authService.Login(model);
            return Redirect("/Home/Index");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _authService.GetAll();
            return Ok(response);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAuthView model)
        {
            var response = await _authService.Register(model);
            return RedirectToAction("LogIn", "Auth", response);
        }
        
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _authService.LogOut();
            return Redirect("/");
        }
    }
}
