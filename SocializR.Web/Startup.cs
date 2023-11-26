using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RouteJs;
using SocializR.DataAccess;
using SocializR.DataAccess.UnitOfWork;
using SocializR.Entities;
using SocializR.Services;
using SocializR.Web.Code.Configuration;
using SocializR.Web.Code.ExtensionMethods;
using System;

namespace SocializR.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            configurationBuilder.AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            var appSettings = Configuration.GetSection("AppSettings");
            var azureImageStorageSettings = Configuration.GetSection("AzureImageStorageSettings");
            services.Configure<AppSettings>(appSettings);
            services.Configure<AzureImageStorageSettings>(azureImageStorageSettings);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<SocializRContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging(true));

            services.AddIdentity<User, Role>(options=>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
                .AddEntityFrameworkStores<SocializRContext>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddRouteJs();

            services.AddScoped<SocializRUnitOfWork>();
            services
                .AddMvc(config =>
                {
                });

            services.AddBusinessLogic(HostingEnvironment);
            services.AddAutoMapper();

            services.AddAuthentication("SocializRCookies")
                    .AddCookie("SocializRCookies", options =>
                    {
                        options.AccessDeniedPath = new PathString("/Account/Login");
                        options.LoginPath = new PathString("/Account/Login");
                    });
            services.AddCurrentUser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   "default",
                   "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}