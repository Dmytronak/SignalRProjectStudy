using Microsoft.Extensions.DependencyInjection;
using SignalRProject.BusinessLogic.Fabrics;
using SignalRProject.BusinessLogic.Fabrics.Interfaces;
using SignalRProject.BusinessLogic.Helpers;
using SignalRProject.BusinessLogic.Helpers.Interfaces;
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
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageInRoomRepository, MessageInRoomRepository>();
            services.AddTransient<IUserInRoomRepository, UserInRoomRepository>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IChatService, ChatService>();

            services.AddTransient<IDateTimeHelper, DateTimeHelper>();
            
            services.AddTransient<IJwtProvider, JwtProvider>();
            services.AddTransient<IImageProvider, ImageProvider>();

            services.AddTransient<IChatMenuItemsFabric, ChatMenuItemsFabric>();
        }
    }
}
