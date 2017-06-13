using System;
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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Omack.Data.DAL;

namespace Omac.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.User.RequireUniqueEmail = true;
                //config.Password.RequiredLength = 8;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                //config.Cookies.ApplicationCookie.LoginPath = "/"; //redirect user to this url if the user is not logged in
            }).AddEntityFrameworkStores<OmackContext, int>();
            services.AddScoped<OmackContext>();
            services.AddScoped<IItemService, ItemService>();   // ItemService:  IItemService,  OtherService:  IItemService
            //services.AddScoped<ItemService>();
            //services.AddScoped<IItemService, ItemService>();  //Scoped - one object for all request from specific client.
            //services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<UnitOfWork>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
