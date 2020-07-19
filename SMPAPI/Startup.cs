using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SMP.Bal.Logic;
using SMP.BAL.DTO;
using SMP.BAL.ILogic;
using SMP.BAL.Logic;
using SMP.Data.ModelForTables;

using SMP.DATA.Models;
using SMP.DATA.Seeder;
using SMP.Repository.Repository;
using SMPAPI.Helpers;
using SMPAPI.Model;
using SMPAPI.Services;
using SMPAPI.Settings;
using SSRFACE.BAL.AutoMappingcls;
using SSRFACE.BAL.DTO;
using SSRFACE.BAL.ILogic;
using SSRFACE.BAL.Logic;
using SSRFACE.DATA.ModelForTables;

namespace SMPAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Inject AppSettings

            services.AddAutoMapper(typeof(AutoMapping));
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddDbContext<SSRFACEDBCONTEXT>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            IdentityHelper.ConfigureService(services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            SwaggerHelper.ConfigureService(services);

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //   services.AddEntityFrameworkSqlServer()

            //.AddDbContext<SMPDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // Get App Settings Configuration

            //var appSettings = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettingsDTO>(appSettings);

            //services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    options.User.RequireUniqueEmail = false;
            //})
            //.AddEntityFrameworkStores<SMPDbContext>()
            //.AddDefaultTokenProviders();


            // Settings
            services.Configure<FileUploading>(Configuration.GetSection("FileUploading"));
            services.Configure<EmailSettings>(Configuration.GetSection("Email"));
            services.Configure<ClientAppSettings>(Configuration.GetSection("ClientApp"));
            services.Configure<JwtSecurityTokenSettings>(Configuration.GetSection("JwtSecurityToken"));

            #region Add Dependency For Repository


            services.AddScoped<IRepository<Users>, Repository<Users>>();

            services.AddScoped<IRepository<PostDto>, Repository<PostDto>>();

            services.AddScoped<IRepository<CommentsDTO>, Repository<CommentsDTO>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<SubcommentOrReplyDTO>, Repository<SubcommentOrReplyDTO>>();
            services.AddScoped<IRepository<SubComment>, Repository<SubComment>>();
            services.AddScoped<IRepository<UserPost>, Repository<UserPost>>();
            services.AddScoped<IRepository<Users>, Repository<Users>>();
            services.AddScoped<IRepository<UserSSODTO>, Repository<UserSSODTO>>();


            // Services
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<Ipost, PostLogic>();
            services.AddTransient<Icomment, CommentLogic>();
            services.AddTransient<Iuserpostorstatus, userpostorstatusLogic>();
            services.AddTransient<IAccount, AccountLogic>();


            #endregion

            #region Add Dependency For Services
            services.AddTransient<IRepository<ExceptionLogger>, Repository<ExceptionLogger>>();




            //Seeder
            services.AddTransient<SMPSeed>();

            services.AddSingleton<IConfiguration>(Configuration);

            #endregion


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();

            UpdateDatabase(app);
            app.UseCors(builder =>
         builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
         .AllowAnyHeader()
         .AllowAnyMethod()

         );
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
            //    c.RoutePrefix = "";
            //});

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=UserDashboard}/{action=newsfeed}/{id?}");
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });


        }


        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SSRFACEDBCONTEXT>())
                {
                    context.Database.Migrate();
                }
            }
        }

    }
}
