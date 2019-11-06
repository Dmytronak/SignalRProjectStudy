using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRProject.BusinessLogic.Providers;
using SignalRProject.BusinessLogic.Providers.Interfaces;
using SignalRProject.BusinessLogic.Services;
using SignalRProject.BusinessLogic.Services.Interfaces;
using SignalRProject.DataAccess.Repositories.EntityFramework;
using SignalRProject.DataAccess.Repositories.Interfaces;

namespace SignalRProject.BusinessLogic
{
    public static class DependencyConfiguration
    {
        public static void AddDependencyConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtProvider, JwtProvider>();
        }
    }
}
