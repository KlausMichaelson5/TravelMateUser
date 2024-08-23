using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Services;
using RoomBookingDll.Models;

namespace RoomBooking.Controllers
{
  [Route("api/rooms")]
        [ApiController]
        [EnableCors("cors")]
        public class RoomController : ControllerBase
        {
            private readonly IRoomBookingDataService _roomDataService;

            public RoomController(IRoomBookingDataService roomDataService)
            {
                _roomDataService = roomDataService;
            }

            [HttpGet("AllRooms")]
            public async Task<ActionResult<List<Room>>> GetAllRooms()
            {
                try
                {
                    var rooms = await _roomDataService.GetAllRoomsAsync();
                    return Ok(rooms);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Room>> GetRoomById(int id)
            {
                try
                {
                    var room = await _roomDataService.GetRoomByIdAsync(id);
                    return Ok(room);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            [HttpGet("AvailableRooms")]
            public async Task<ActionResult<List<Room>>> GetAvailableRooms()
            {
                try
                {
                    var rooms = await _roomDataService.GetAvailableRoomsAsync();
                    return Ok(rooms);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            [HttpPost]
            public async Task<IActionResult> AddRoom([FromBody] Room room)
            {
                try
                {
                    await _roomDataService.AddRoomAsync(room);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
            {
                try
                {
                    var existingRoom = await _roomDataService.GetRoomByIdAsync(id);
                    if (existingRoom == null)
                    {
                        return NotFound($"Room with id {id} not found.");
                    }

                    room.RoomId = id; // Ensure the ID matches the route parameter
                    await _roomDataService.UpdateRoomAsync(room);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRoom(int id)
            {
                try
                {
                    await _roomDataService.DeleteRoomByIdAsync(id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }

