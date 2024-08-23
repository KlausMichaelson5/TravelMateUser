using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TravelMate.Services;
using UserDll.Models;
using UserDll.Services;

namespace UserWebApi
{
	public class Program
	{
		public static void ConfigureServices(IServiceCollection services)
		{
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            const string ApiKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var key = Encoding.ASCII.GetBytes(ApiKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


            //services.AddDbContext<UserDbContext>(options =>
            //{
            //    options.UseSqlServer(services.Configuration.GetConnectionString("myConnection"));
            //});
            services.AddTransient<IUserDataService, UserDataService>();
            services.AddTransient<IAdminDataService, AdminDataService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAdminService, AdminService>();
        }
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           ConfigureServices(builder.Services);

			builder.Services.AddControllers();
			builder.Services.AddDbContext<UserDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//var key = Encoding.ASCII.GetBytes("your_secret_key_here");
			//builder.Services.AddAuthentication(options =>
			//{
			//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			//}).AddJwtBearer(options =>
			//{
			//	options.TokenValidationParameters = new TokenValidationParameters
			//	{
			//		ValidateIssuerSigningKey = true,
			//		IssuerSigningKey = new SymmetricSecurityKey(key),
			//		ValidateIssuer = false,
			//		ValidateAudience = false
			//	};
			//});

			builder.Services.AddAuthorization();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("cors");
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
