using System.Globalization;
using Careers.EF;
using Careers.Services;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Careers
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
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddDbContext<CareersDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddTransient<LocationsService>();

            services.AddMvc()
                .AddRazorRuntimeCompilation()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
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

            var cultures = new[] {
                new CultureInfo ("ru-RU"),
                new CultureInfo ("az-Latn-AZ")
            };

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization(options => {
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
            });

            app.UseRouting();

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
