
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using TaskItApi.Handlers;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Helper;
using TaskItApi.Models;
using TaskItApi.Models.Interfaces;
using TaskItApi.Repositories;
using TaskItApi.Repositories.Interfaces;
using TaskItApi.Resources;
using TaskItApi.Services;
using TaskItApi.Services.Interfaces;

namespace TaskItApi
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
            services.AddDbContext<TaskItDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Server"))
                );

            InitDependicyInjection(services);


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOption => {
                jwtBearerOption.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                            .GetBytes(Configuration.GetSection("AppSettings:AppSecret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                      };
                });

            InitSwaggerGent(services);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false)
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(ApiResponse));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TaskItDbContext>();
                context.Database.EnsureCreated();
            }

            app.UseCors();

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("nl")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("nl"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures              
            });

            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskIt Api");
            });

            app.UseAuthentication();
            app.UseMvc();
        }

        private void InitDependicyInjection(IServiceCollection services)
        {
            //Handlers
            services.AddTransient<IEmailHandler, EmailHandler>();
            services.AddTransient<ITokenHandler, Handlers.TokenHandler>();

            //Helpers
            services.AddTransient<IResourcesHelper, ResourcesHelper>();

            //Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IDefaultService, DefaultService>();
            services.AddTransient<ITaskService, TaskService>();
            
            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IIconRepository, IconRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITaskHolderRepository, TaskHolderRepository>();

            //Models
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private void InitSwaggerGent(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskIt Api", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
