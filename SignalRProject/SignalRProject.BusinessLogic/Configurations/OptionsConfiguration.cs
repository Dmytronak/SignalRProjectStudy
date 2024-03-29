﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRProject.BusinessLogic.Options;

namespace SignalRProject.BusinessLogic.Configurations
{
    public static class OptionsConfiguration
    {
        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services
                .Configure<JwtOption>(configuration.GetSection("Jwt"));
        }
    }
}
