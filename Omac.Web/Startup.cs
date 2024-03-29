﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Omack.Services.ServiceImplementations;
using Omack.Data.Infrastructure;
using Omack.Services.Services;
using Omack.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Omack.Data.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Omack.Web.Authorization;
using Omack.Web.Site;
using Omack.Web.AppStart;
using NLog.Web;
using NLog.Extensions.Logging;
using NLog;

namespace Omac.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            env.ConfigureNLog("nlog.config"); //nlog configuration file
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddSingleton(Configuration); // if we need to access it's content outside, user this service.

            var autoConfig = new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile(new ApplicationProfile());
            });
            var mapper = autoConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddIdentity<User, Role>(config =>
            {
                config.User.RequireUniqueEmail = true;
                //config.Password.RequiredLength = 8;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                //redirect user to this url if the user is not logged in
                config.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                config.Cookies.ApplicationCookie.AccessDeniedPath = "/Account/AccessDenied";
                config.Cookies.ApplicationCookie.AutomaticAuthenticate = true; //bring in identity from the cookie
                config.Cookies.ApplicationCookie.AutomaticChallenge = true; // resolve 401 403 status codes to page specified above.  
                config.Cookies.ApplicationCookie.ExpireTimeSpan = new TimeSpan(1,0,0);
            }).AddEntityFrameworkStores<OmackContext, int>();
            services.AddAuthorization(options =>
            {
 
                options.AddPolicy("Admin", policy =>
                {
                    policy.Requirements.Add(new IsGroupAdmin());
                   // policy.RequireClaim("Admin", "Admin");
                });
                //options.AddPolicy("Over18", policy => policy.Requirements.Add());
            });
            services.AddDbContext<OmackLogContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Omack-Log")));
            services.AddDbContext<OmackContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Omack-Dev")));
            //Scoped - one object for all request from specific client.
            // ItemService:  IItemService,  OtherService:  IItemService 
            services.AddSingleton<UserService>();
            services.AddSingleton<IAuthorizationHandler, IsGroupAdminHandler>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<GroupService>();
            services.AddScoped<UnitOfWork>();
            services.AddScoped<SiteUtils>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {            
            loggerFactory.AddNLog();
            LogManager.Configuration.Variables["Omack-Log"] = Configuration.GetConnectionString("Omack-Log");
            if (env.IsDevelopment())
            {
                //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                //loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.AddNLogWeb();
            app.UseIdentity();
            app.UseStaticFiles();            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
