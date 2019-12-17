using System;
using System.Globalization;
using Careers.EF;
using Careers.Models.Identity;
using Careers.Services;
using Careers.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using Careers.Repositories;
using Microsoft.AspNetCore.Http;

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
            //services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromHours(10);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            services.AddIdentity<AppUser, IdentityRole>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<CareersDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddDbContextPool<CareersDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            //services
            services.AddTransient<LocationService>();
            services.AddTransient<SenderService>();
            services.AddTransient<SmsService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IMeetingPointService, MeetingPointService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IOrderService, OrderSercice>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ISpecialistService, SpecialistService>();

            services.AddScoped<MediaRepository>();


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
            // app.UseStatusCodePagesWithRedirects("/Home/Error");

            var cultures = new[] {
                new CultureInfo ("ru-RU"),
                new CultureInfo ("az-Latn-AZ")
            };

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization(options =>
            {
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
