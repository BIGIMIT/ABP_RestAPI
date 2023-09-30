
using ABP_RestAPI.Models;
using ABP_RestAPI.Services.Experiments;
using Microsoft.EntityFrameworkCore;

namespace ABP_RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton(serviceProvider =>
            {
                var experiments = new Dictionary<string, IExperiment>
                {
                    { "button-color", new ButtonExperiment() },
                    { "price", new PriceExperiment() },
                };
                return experiments;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.MapControllers();
            
            app.Run();
        }
    }
}