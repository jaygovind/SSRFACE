﻿using AutoMapper;
using Data.DataAccessLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SimpleCRUDExample.Core.Configurations;
using SimpleCRUDExample.Core.Models.ConfigModels;

namespace SimpleCRUDExample
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                //SqliteConnection inMemorySqlite = new SqliteConnection(_configuration.GetConnectionString("DevConnection"));
                //inMemorySqlite.Open();

                //services.AddDbContext<CRUDAppContext>(options =>
                //    options.UseSqlite(inMemorySqlite));
            }

            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);


            services.AddDbContext<CRUDAppContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DevConnection")));



            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });


            services.ConfigureAuthService();
            services.ConfigureCookieService();
            services.Configure<LoginConfigModel>(_configuration.GetSection("DevLoginConfig"));

            services.ConfigureBusinessEngines();
            services.ConfigureCoreModules();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CRUDAppContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            context.Database.Migrate();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=newsfeed}/{id?}");
            });
        }
    }
}
