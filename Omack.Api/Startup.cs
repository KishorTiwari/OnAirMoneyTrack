﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using NLog.Web;
using Omack.Data.Infrastructure;
using Omack.Data.DAL;
using NLog;
using Omack.Services.ServiceImplementations;
using Omack.Services.Services;

namespace Omack.Api
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
            env.ConfigureNLog("nlog.config"); //ConfigureNlog is extention method from Nlog to get configuration details from "nlog.config" file.
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //add MVC with some modifications if needed.
            services.AddMvc()
                    .AddMvcOptions(o =>
                    {
                        o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    });
            services.AddDbContext<OmackContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Omack-Dev")));
            services.AddDbContext<OmackLogContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Omack-Log")));
            services.AddScoped<UnitOfWork>();
            services.AddScoped<GroupService>();
            //sets the default camelcase format for returned json result to null, which will finally depened upon C# object's property names
            //.AddJsonOptions(o =>
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;     //sets the defaults to null
            //    }
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug(); //by default: Information level or more serious. to log critical error .AddDebug(LogLevel.Critical).            
            loggerFactory.AddNLog(); //buildin extension for Nlog
            LogManager.Configuration.Variables["Omack-Log"] = Configuration.GetConnectionString("Omack-Log");
            app.AddNLogWeb();
            app.UseStatusCodePages();  //to show status code to the browser. Otherwise we have to look through console to inspect status code.
            app.UseMvc();
        }
    }
}
