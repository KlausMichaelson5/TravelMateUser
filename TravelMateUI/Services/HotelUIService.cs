using System.Net.Http.Json;
using TravelMate.Models;
using TravelMateUI;

namespace TravelMate2.Services
{
	public interface IHotelUIService
	{
		Task Add(Hotel hotel, int currentUserId);
		Task<Hotel> Get(int id, int currentUserId);
		Task Update(Hotel hotel, int currentUserId);
		Task Delete(int id, int currentUserId);
		Task<List<Hotel>> GetAll(int currentUserId);
	}

	public class HotelUIService : IHotelUIService
	{
		private readonly HttpClient _httpClient;

		public HotelUIService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:5043/api/");
		}

		public async Task Add(Hotel hotel, int currentUserId)
        {
			await _httpClient.PostAsJsonAsync($"hotels?currentUserId={currentUserId}",hotel);
		}

		public async Task<Hotel> Get(int id, int currentUserId)
		{
			var response = await _httpClient.GetAsync($"hotels/{id}?currentUserId={currentUserId}");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<Hotel>();
			}
			else
			{
				throw new Exception("Hotel not found");
			}
		}

		public async Task Update(Hotel hotel, int currentUserId)
		{
			await _httpClient.PutAsJsonAsync($"hotels/{hotel.HotelOwnerId}?currentUserId={currentUserId}", hotel);
		}

		public async Task Delete(int id, int currentUserId)
		{
			await _httpClient.DeleteAsync($"hotels/{id}?currentUserId={currentUserId}");
		}

		public async Task<List<Hotel>> GetAll(int currentUserId)
		{
			return await _httpClient.GetFromJsonAsync<List<Hotel>>($"hotels/AllHotels?currentUserId={currentUserId}");
		}
	}
}