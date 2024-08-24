using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelMate.Models;

namespace TravelMate2.Services
{
	public interface ICabUIService
	{
		Task AddCab(Cab cab, int userId);
		Task DeleteCab(int cabId, int userId);
		Task<Cab> GetCab(int userId);
		Task UpdateCab(Cab cab, int userId);
	}

	public class CabUIService : ICabUIService
	{
		private readonly HttpClient httpClient;

		public CabUIService(HttpClient client)
		{
			this.httpClient = client;
            httpClient.BaseAddress = new Uri("http://localhost:5034/api/");
		}

		public async Task<Cab> GetCab(int userId)
		{
			return await httpClient.GetFromJsonAsync<Cab>($"Cab/{userId}");
		}

		public async Task AddCab(Cab cab, int userId)
		{
			await httpClient.PostAsJsonAsync($"Cab/?currentUserId={userId}", cab);
		}

		public async Task UpdateCab(Cab cab, int userId)
		{
			await httpClient.PutAsJsonAsync($"Cab/?currentUserId={userId}", cab);
		}

		public async Task DeleteCab(int cabId, int userId)
		{
			await httpClient.DeleteAsync($"Cab/{cabId}?currentUserId={userId}");
		}
	}
}