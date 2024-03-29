﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.ViewModels.AuthViews;

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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAuthView model)
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAuthView model)
        {
            await _authService.Register(model);
            return Redirect("~/Auth/Login");
        }
        
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _authService.LogOut();
            return Redirect("/");
        }
    }
}
