using HotelDll.Models;
using HotelDll.Services;

namespace HotelWebApi.Services
{
    public interface IHotelDataService
    {
        Task AddHotel(Hotel hotel, int currentUserId);
        Task DeleteHotelById(int id, int currentUserId);
        Task<Hotel> GetHotelById(int id, int currentUserId);
        Task<List<Hotel>> GetAllHotels(int currentUserId);
        Task UpdateHotel(Hotel hotel, int currentUserId);
    }
    public class HotelDataService : IHotelDataService
    {
        private readonly IHotelService _hotelService;

        public HotelDataService(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public Task<List<Hotel>> GetAllHotels(int currentUserId)
        {
            return Task.Run(() => _hotelService.GetAllHotels(currentUserId));
        }

        public Task<Hotel> GetHotelById(int id, int currentUserId)
        {
            return Task.Run(() => _hotelService.GetHotelById(id, currentUserId));
        }

        public Task AddHotel(Hotel hotel, int currentUserId)
        {
            return Task.Run(() => _hotelService.AddNewHotel(hotel, currentUserId));
        }

        public Task UpdateHotel(Hotel hotel, int currentUserId)
        {
            return Task.Run(() => _hotelService.UpdateHotel(hotel, currentUserId));
        }

        public Task DeleteHotelById(int id, int currentUserId)
        {
            return Task.Run(() => _hotelService.DeleteHotel(id, currentUserId));
        }
    }
}
