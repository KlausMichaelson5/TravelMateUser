using HotelDll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelDll.Services
{
    public interface IHotelService
    {
        void AddNewHotel(Hotel hotel, int currentUserId);
        void DeleteHotel(int id, int currentUserId);
        Hotel GetHotelById(int id, int currentUserId);
        List<Hotel> GetAllHotels(int currentUserId);
        void UpdateHotel(Hotel hotel, int currentUserId);
    }

    public class HotelService : IHotelService
    {
        private readonly HotelDbContext _context;

        public HotelService(HotelDbContext context)
        {
            _context = context;
        }

        public List<Hotel> GetAllHotels(int currentUserId)
        {
            return _context.Hotels
                .Where(h => h.HotelOwnerId == currentUserId)
                .Select(hotelEntity => new Hotel
                {
                    HotelId = hotelEntity.HotelId,
                    HotelOwnerId = hotelEntity.HotelOwnerId,
                    Name = hotelEntity.Name,
                    Address = hotelEntity.Address,
                    City = hotelEntity.City,
                    State = hotelEntity.State,
                    Country = hotelEntity.Country,
                    ZipCode = hotelEntity.ZipCode,
                    Description = hotelEntity.Description,
                    Latitude = hotelEntity.Latitude,
                    Longitude = hotelEntity.Longitude,
                    Rating = hotelEntity.Rating,
                    AvailabilityStatus = hotelEntity.AvailabilityStatus,
                    HotelImage = hotelEntity.HotelImage
                })
                .ToList();
        }

        public Hotel GetHotelById(int id, int currentUserId)
        {
            var hotelEntity = _context.Hotels.Find(id);

            if (hotelEntity == null || hotelEntity.HotelOwnerId != currentUserId)
            {
                throw new Exception($"Hotel with id {id} not found or you do not have access.");
            }

            return new Hotel
            {
                HotelId = hotelEntity.HotelId,
                HotelOwnerId = hotelEntity.HotelOwnerId,
                Name = hotelEntity.Name,
                Address = hotelEntity.Address,
                City = hotelEntity.City,
                State = hotelEntity.State,
                Country = hotelEntity.Country,
                ZipCode = hotelEntity.ZipCode,
                Description = hotelEntity.Description,
                Latitude = hotelEntity.Latitude,
                Longitude = hotelEntity.Longitude,
                Rating = hotelEntity.Rating,
                AvailabilityStatus = hotelEntity.AvailabilityStatus,
                HotelImage = hotelEntity.HotelImage
            };
        }

        public void UpdateHotel(Hotel hotel, int currentUserId)
        {
            var existingHotel = _context.Hotels.Find(hotel.HotelId);

            if (existingHotel == null || existingHotel.HotelOwnerId != currentUserId)
            {
                throw new Exception("Hotel not found or you do not have access to update.");
            }

            existingHotel.Name = hotel.Name;
            existingHotel.Address = hotel.Address;
            existingHotel.City = hotel.City;
            existingHotel.State = hotel.State;
            existingHotel.Country = hotel.Country;
            existingHotel.ZipCode = hotel.ZipCode;
            existingHotel.Description = hotel.Description;
            existingHotel.Latitude = hotel.Latitude;
            existingHotel.Longitude = hotel.Longitude;
            existingHotel.Rating = hotel.Rating;
            existingHotel.AvailabilityStatus = hotel.AvailabilityStatus;
            existingHotel.HotelImage = hotel.HotelImage;

            _context.SaveChanges();
        }

        public void AddNewHotel(Hotel hotel, int currentUserId)
        {
            var newHotel = new Hotel
            {
                HotelOwnerId = currentUserId,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                State = hotel.State,
                Country = hotel.Country,
                ZipCode = hotel.ZipCode,
                Description = hotel.Description,
                Latitude = hotel.Latitude,
                Longitude = hotel.Longitude,
                Rating = hotel.Rating,
                AvailabilityStatus = hotel.AvailabilityStatus,
                HotelImage = hotel.HotelImage
            };

            _context.Hotels.Add(newHotel);
            _context.SaveChanges();
        }

        public void DeleteHotel(int id, int currentUserId)
        {
            var hotel = _context.Hotels.Find(id);

            if (hotel == null || hotel.HotelOwnerId != currentUserId)
            {
                throw new Exception("Hotel not found or you do not have access to delete.");
            }

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }
    }
}
