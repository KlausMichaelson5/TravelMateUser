using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TravelMate2.Services;

namespace TravelMateUI
{
    public class Program
    {
		public static IConfigurationRoot Configuration { get; set; }
		public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
			Configuration = builder.Configuration;
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseUrl"]) });
			builder.Services.AddScoped<AuthService>();
			builder.Services.AddScoped<UserInfoService>();
			builder.Services.AddTransient<IUserUIService, UserUIService>();
            builder.Services.AddTransient<IHotelUIService, HotelUIService>();
            builder.Services.AddTransient<ICabUIService, CabUIService>();
            await builder.Build().RunAsync();
        }
    }
}
