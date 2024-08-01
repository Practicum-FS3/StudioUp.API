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
using System.Text.Json.Serialization;

namespace StudioUp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .WithExposedHeaders("Content-Disposition")
                                      .WithExposedHeaders("Access-Control-Allow-Origin");
                                  });

            });
            // Configuration
            builder.Configuration.AddJsonFile("appsettings.json", optional: false);

            // Database context
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StudioUp")));

            // Repositories
            builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();

            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            //});
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
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
            builder.Services.AddAutoMapper(typeof(MappingProfile)); 


            //builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddScoped<IHMORepository, HMORepository>();
            builder.Services.AddScoped<IAvailableTrainingRepository, AvailableTrainingRepository>();
            builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ITrainingCustomerTypeRepository, TrainingCustomerTypeRepository>();

            builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
            builder.Services.AddScoped<IRepository<CustomerType>, CustomerTypeRepository>();
            builder.Services.AddScoped<IRepository<SubscriptionType>, SubscriptionTypeRepository>();
            builder.Services.AddScoped<IRepository<PaymentOption>, PaymentOptionRepository>();
            builder.Services.AddScoped<IContentSectionRepository, ContentSectionRepository>();
            builder.Services.AddScoped<IRepository<TrainingType>, TrainigTypeRepository>();
            builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();
            builder.Services.AddScoped<ILeumitCommimentsRepository, LeumitCommimentRepository>();
            builder.Services.AddScoped<ILeumitCommimentTypesRepository, LeumitCommimentTypesRepository>();
            builder.Services.AddScoped<ITrainingCustomerRepository, TrainingCustomerRepository>();
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
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });


            var app = builder.Build();

            // Middleware setup
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //cors
            app.UseCors("AllowOrigin");



            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
