﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRProject.BusinessLogic.Providers.Interfaces;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.DataAccess.Entities;
using SignalRProject.ViewModels.AuthViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        #region Properties

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IImageProvider _imageProvider;
        #endregion Properties

        #region Constructor
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IJwtProvider jwtProvider, IImageProvider imageProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _imageProvider = imageProvider;
        }

        #endregion Constructor

        #region Public Methods
        public async Task<GetAllAuthView> GetAll()
        {
            List<User> users = await _userManager.Users.ToListAsync();
            if (!users.Any())
            {
                throw new Exception("User is not found");
            }
            GetAllAuthView response = new GetAllAuthView()
            {
                Users = users
                .Select(x => new UserGetAllAuthViewItem()
                {
                    Age = x.Age,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email
                })
                .ToList()
            };
            return response;
        }

        public async Task<GetUserView> GetbyId(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User is not found");
            }

            GetUserView response = new GetUserView
            {
                Age = user.Age,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Photo = user.Photo
            
            };
            return response;
        }

        public async Task<LoginAuthResponseView> Register(RegisterAuthView model)
        {
            User user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            if (model.Photo!=null)
            {
                user.Photo = _imageProvider.ResizeAndSave(model.Photo, Constants.FilePaths.UserAvatarImages, $"{user.Id}.png", Constants.DefaultIconSizes.MaxWidthOriginalImage, Constants.DefaultIconSizes.MaxHeightOriginalImage);
            }

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Registration is not complete");
            }

            string encodedJwt = await _jwtProvider.GenerateJwtToken(user);
            LoginAuthResponseView response = new LoginAuthResponseView
            {
                Token = encodedJwt
            };
            return response;
        }

        public async Task<LoginAuthResponseView> Login(LoginAuthView model)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid Login or password");

            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var encodedJwt = await _jwtProvider.GenerateJwtToken(user);
            var response = new LoginAuthResponseView()
            {
                Token = encodedJwt
            };
            return response;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();

            if (_signInManager.Context.User?.Identity.IsAuthenticated == true)
            {
                _signInManager.Context.Response.Cookies.Delete(".SignalRProjectCookieName");
            }
        }

        #endregion Public Methods

    }
}
