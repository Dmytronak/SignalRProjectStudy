using Microsoft.Extensions.DependencyInjection;


namespace SignalRProject.BusinessLogic.Configurations
{
    public static class SignalRConfiguration
    {
        public static void AddSignalRConfiguration(this IServiceCollection services)
        {
            services.AddSignalR();
        } 
    }
}
