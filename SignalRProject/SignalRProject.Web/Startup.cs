using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRProject.BusinessLogic;
using SignalRProject.BusinessLogic.Configurations;
using SignalRProject.BusinessLogic.HubServices;
using SignalRProject.Web.Filters;
using SignalRProject.Web.Middlewares;

namespace SignalRProject.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddOptionsConfiguration(Configuration);
            services.AddDependencyConfiguration();
            services.AddDatabaseContextConfiguration(Configuration);
            services.AddIdentityConfiguration();
            services.AddSignalRConfiguration();
            services.AddJwtConfiguration(Configuration);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelStateActionFilter));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<HubChatService>("/chatHub");

            });

        }
    }
}
