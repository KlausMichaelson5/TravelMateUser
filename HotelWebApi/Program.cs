
using HotelDll.Models;
using HotelDll.Services;
using HotelWebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebApi
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<IHotelService, HotelService>();
            

            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cors", setUp =>
                {
                    setUp.AllowAnyHeader();
                    setUp.AllowAnyMethod();
                    setUp.AllowAnyOrigin();
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HotelDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("cors");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
