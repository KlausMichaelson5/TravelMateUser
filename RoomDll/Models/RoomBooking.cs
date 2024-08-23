using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RoomBookingDll.Models
{
    [Table("tblRooms")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoomType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(20)]
        public string AvailabilityStatus { get; set; }

        [MaxLength(200)]
        public string Amenities { get; set; }
    }

    public class RoomBookingDbContext : DbContext
    {
        public RoomBookingDbContext() { }
        public RoomBookingDbContext(DbContextOptions<RoomBookingDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
    }
}
