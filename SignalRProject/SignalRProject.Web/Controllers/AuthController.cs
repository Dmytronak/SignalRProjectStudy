using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginAuthView model)
        {
            var response = await _authService.Login(model);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _authService.GetAll();
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterAuthView model)
        {
            var response = await _authService.Register(model);
            return Ok(response);
        }
    }
}
