using HotelDll.Models;
using HotelDll.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace HotelWebApi.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    [EnableCors("cors")]
    public class HotelController : ControllerBase
    {
      private readonly IHotelService _hotelService;

        public HotelController(IHotelService service)
        {
            _hotelService = service;
        }

        [HttpGet("AllHotels")]
        public async Task<ActionResult<List<Hotel>>> GetAllHotel(int currentUserId)
        {
            try
            {
                var hotels = _hotelService.GetAllHotels(currentUserId);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotelById(int id, int currentUserId)
        {
            try
            {
                var hotel = _hotelService.GetHotelById(id, currentUserId);
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewHotel([FromBody] Hotel hotel, int currentUserId)
        {
            try
            {
                _hotelService.AddNewHotel(hotel, currentUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel, int currentUserId)
        {
            try
            {
                _hotelService.UpdateHotel(hotel, currentUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id, int currentUserId)
        {
            try
            {
                _hotelService.DeleteHotel(id, currentUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
