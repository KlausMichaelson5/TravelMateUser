using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMate.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [ForeignKey("User")]
        public int HotelOwnerId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public Nationality Country { get; set; }

        public string ZipCode { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal Rating { get; set; }

        public bool AvailabilityStatus { get; set; }

        public string HotelImage { get; set; } = string.Empty;
    }
}
