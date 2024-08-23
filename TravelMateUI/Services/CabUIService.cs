using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelMate.Models;

namespace TravelMate2.Services
{
	public interface ICabUIService
	{
		Task AddCab(Cab cab, int userId);
		Task DeleteCab(int cabId, int userId);
		Task<Cab> GetCab(int cabId, int userId);
		Task UpdateCab(Cab cab, int userId);
	}

	public class CabUIService : ICabUIService
	{
		private readonly HttpClient httpClient;

		public CabUIService(HttpClient client)
		{
			this.httpClient = client;
		}

		public async Task<Cab> GetCab(int cabId, int userId)
		{
			return await httpClient.GetFromJsonAsync<Cab>($"cabs/{cabId}?currentUserId={userId}");
		}

		public async Task AddCab(Cab cab, int userId)
		{
			await httpClient.PostAsJsonAsync($"cabs/addcab?currentUserId={userId}", cab);
		}

		public async Task UpdateCab(Cab cab, int userId)
		{
			await httpClient.PutAsJsonAsync($"cabs/?currentUserId={userId}", cab);
		}

		public async Task DeleteCab(int cabId, int userId)
		{
			await httpClient.DeleteAsync($"api/cabs/{cabId}?currentUserId={userId}");
		}
	}
}