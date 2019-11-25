using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SignalRProject.BusinessLogic.AutoMapperProfiles;

namespace SignalRProject.BusinessLogic.Configurations
{
    public static class AutoMapperConfiguration
    {
       public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            #region Register Mappers

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ChatAutoMapperProfile());
                cfg.AddProfile(new MenuAutoMapperProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            #endregion
        }
    }
}
