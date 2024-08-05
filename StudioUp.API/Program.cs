using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using StudioUp.Repo.Repository;
using System.Text;
using NLog;
using NLog.Web;
using StudioUp.DTO;
namespace StudioUp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddControllersWithViews();
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
                builder.Services.AddControllers();
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowOrigin",
                        builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader());
                });
                //builder.Services.AddCors(options =>
                //{
                //    options.AddPolicy(name: MyAllowSpecificOrigins,
                //                      policy =>
                //                      {
                //                          policy.WithOrigins("http://localhost:3000")
                //                          .AllowAnyMethod()
                //                          .AllowAnyHeader()
                //                          .WithExposedHeaders("Content-Disposition")
                //                          .WithExposedHeaders("Access-Control-Allow-Origin");
                //                      });
                //});
                // Configuration
                builder.Configuration.AddJsonFile("appsettings.json", optional: false);
                // Database context
                builder.Services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("StudioUp")));
                // Repositories
                builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
                builder.Services.AddControllers().AddJsonOptions(options =>
                {
                   
                });
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = builder.Configuration["JWT:Issuer"],
                      ValidAudience = builder.Configuration["JWT:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                  };
              });
                // Add services to the container
                builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
                builder.Services.AddScoped<IContentTypeRepository, ContentTypeRepository>();
              //  builder.Services.AddAutoMapper(typeof(MappingProfile));
               // builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddScoped<IHMORepository, HMORepository>();
                builder.Services.AddScoped<IAvailableTrainingRepository, AvailableTrainingRepository>();
                builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
                builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
                builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
                builder.Services.AddScoped<IRepository<CustomerTypeDTO>, CustomerTypeRepository>();
                builder.Services.AddScoped<IRepository<SubscriptionTypeDTO>, SubscriptionTypeRepository>();
                builder.Services.AddScoped<IRepository<PaymentOptionDTO>, PaymentOptionRepository>();
                builder.Services.AddScoped<IContentSectionRepository, ContentSectionRepository>();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Description = "Bearer Authentication with JWT Token",
                        Type = SecuritySchemeType.Http
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                     {
                         new OpenApiSecurityScheme
                         {
                          Reference = new OpenApiReference
                          {
                           Id = "Bearer",
                           Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
                });
                // AutoMapper
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                //builder.Services.AddCors(options =>
                //{
                //    options.AddPolicy("AllowOrigin",
                //        builder => builder.AllowAnyOrigin()
                //                          .AllowAnyMethod()
                //                          .AllowAnyHeader());
                //});
                var app = builder.Build();
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
                // Middleware setup
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                //cors
                app.UseCors("AllowOrigin");
                app.MapControllers();
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();
                app.MapControllerRoute(
      name: "default",
      pattern: "{controller=ContentTypeController}/{action=Index}/{id?}");
                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}