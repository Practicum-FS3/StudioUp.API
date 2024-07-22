using Microsoft.EntityFrameworkCore;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using StudioUp.Repo.Repository;

namespace StudioUp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration
            builder.Configuration.AddJsonFile("appsettings.json", optional: false);

            // Database context
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StudioUp")));

            // Repositories
            builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            });

            // Add services to the container
            builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
            builder.Services.AddScoped<IContentTypeRepository, ContentTypeRepository>();
            builder.Services.AddAutoMapper(typeof(MappingProfile)); 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddScoped<IHMORepository, HMORepository>();
            builder.Services.AddScoped<IAvailableTrainingRepository, AvailableTrainingRepository>();
            builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
            builder.Services.AddScoped<IRepository<CustomerType>, CustomerTypeRepository>();
            builder.Services.AddScoped<IRepository<SubscriptionType>, SubscriptionTypeRepository>();
            builder.Services.AddScoped<IRepository<PaymentOption>, PaymentOptionRepository>();
            builder.Services.AddScoped<IContentSectionRepository, ContentSectionRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Middleware setup
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
