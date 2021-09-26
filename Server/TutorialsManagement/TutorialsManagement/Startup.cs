using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorialsManagement.Core.Interfaces;
using TutorialsManagement.DataAccess.Implementations;
using TutorialsManagement.DataAccess.Models;
using TutorialsManagement.Services.Implementations;
using TutorialsManagement.Services.Interfaces;

namespace TutorialsManagement
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbContext, TutorialsManagementDBContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IManageService, ManageService>();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); // use this line instead if in .NET Core 2.1
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0); // use this line instead if in .NET Core 3.1

            // Read Application.json setting
            var settings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(settings);

            // Read DB connection string
            services.AddDbContext<TutorialsManagementDBContext>(options => options.UseSqlServer(settings.GetSection("DBConnectionString").Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()); // this line must right after app.UseRouting, nothing between them

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc();
        }
    }

    public class AppSettings
    {
        public string DBConnectionString { get; set; }
    }
}
