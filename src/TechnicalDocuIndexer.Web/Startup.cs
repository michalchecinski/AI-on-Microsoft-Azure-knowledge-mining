using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechnicalDocuIndexer.Web.Auth0;
using TechnicalDocuIndexer.Web.Models;
using TechnicalDocuIndexer.Web.Service;
using TechnicalDocuIndexer.Web.Service.Utils;

namespace TechnicalDocuIndexer.Web
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

            var auth0Settings = new Auth0Settings();
            Configuration.GetSection(nameof(Auth0Settings)).Bind(auth0Settings);
            services.AddAuth0(auth0Settings);

            services.AddSingleton<IFileUploadHandler, FileUploadHandler>();
            services.AddSingleton<DocumentService, DocumentService>();
            services.AddSingleton<IFileRepository, AzureStorageFileRepository>();
            services.AddControllersWithViews();
            services.Configure<SearchConfigurationModel>(Configuration.GetSection("Search"));
            services.Configure<ConnectionConfigurationModel>(Configuration.GetSection("Connections"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
