using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRProject.BusinessLogic.Configurations
{
    public static class CookiesConfiguration
    {
        public static void AddCookiesConfiguration(this IServiceCollection services)
        {
            //services
            //    .ConfigureApplicationCookie(options =>
            //    {
            //        options.Cookie.Name = ".SignalRProjectCookieName";
            //        options.Cookie.HttpOnly = true;
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //        options.LoginPath = "/Auth/Login";
            //        options.LogoutPath = "/";
            //        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //        options.SlidingExpiration = true;
            //    });                
                             
        }
    }
}
