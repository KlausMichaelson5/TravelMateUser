using RoomBookingDll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingDll.Services
{
    public interface IRoomBookingService
    {
        void AddNewRoom(Room room);
        void DeleteRoom(int roomId);
        Room GetRoomById(int roomId);
        List<Room> GetAllRooms();
        List<Room> GetAvailableRooms();
        void UpdateRoom(Room room);
    }

    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingDbContext _context;

        public RoomBookingService(RoomBookingDbContext context)
        {
            _context = context;
        }

        public void AddNewRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void DeleteRoom(int roomId)
        {
            var room = _context.Rooms.Find(roomId);

            if (room == null)
            {
                throw new Exception("Room not found.");
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }

        public Room GetRoomById(int roomId)
        {
            var room = _context.Rooms.Find(roomId);

            if (room == null)
            {
                throw new Exception($"Room with id {roomId} not found.");
            }

            return room;
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }

        public List<Room> GetAvailableRooms()
        {
            return _context.Rooms
                .Where(r => r.AvailabilityStatus == "Available")
                .ToList();
        }

        public void UpdateRoom(Room room)
        {
            var existingRoom = _context.Rooms.Find(room.RoomId);

            if (existingRoom == null)
            {
                throw new Exception($"Room with id {room.RoomId} not found.");
            }

            // Update the room properties
            existingRoom.RoomType = room.RoomType;
            existingRoom.Price = room.Price;
            existingRoom.AvailabilityStatus = room.AvailabilityStatus;
            existingRoom.Amenities = room.Amenities;

            _context.Rooms.Update(existingRoom);
            _context.SaveChanges();
        }
    }
}
