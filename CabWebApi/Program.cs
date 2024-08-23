using CabDll.Models;
using CabDll.Services;
using Microsoft.EntityFrameworkCore;
using TravelMate.DataService;

namespace TravelMate
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			//builder.Services.AddTransient<IUserService, UserService>();
			builder.Services.AddScoped<ICabService, CabService>();
			builder.Services.AddScoped<CabDataServices>();

			builder.Services.AddDbContext<CabDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
			});

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
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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
