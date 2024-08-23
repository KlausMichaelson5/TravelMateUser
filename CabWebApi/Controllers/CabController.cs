using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelMate.DataService;
using CabDll.Models;

namespace TravelMate.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CabController : ControllerBase
	{
		private readonly CabDataServices _cabDataServices;

		public CabController(CabDataServices cabDataServices)
		{
			_cabDataServices = cabDataServices;
		}

		// GET: api/cab/{currentUserId}
		[HttpGet("{currentUserId}")]
		public async Task<IActionResult> GetCab(int currentUserId)
		{
			try
			{
				var cab = await _cabDataServices.GetCabAsync(currentUserId);
				return Ok(cab);
			}
			catch (Exception ex)
			{
				return NotFound(new { Message = ex.Message });
			}
		}

		// POST: api/cab
		[HttpPost]
		public async Task<IActionResult> AddCab([FromBody] Cab cab, [FromQuery] int currentUserId)
		{
			try
			{
				await _cabDataServices.AddCabAsync(cab, currentUserId);
				return Ok(new { Message = "Cab added successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}

		// PUT: api/cab/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCab(int id, [FromBody] Cab cab, [FromQuery] int currentUserId)
		{
			try
			{
				cab.CabId = id; // Ensure the cab ID in the body matches the ID in the route
				await _cabDataServices.UpdateCabAsync(cab, currentUserId);
				return Ok(new { Message = "Cab updated successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}

		// DELETE: api/cab/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCab(int id, [FromQuery] int currentUserId)
		{
			try
			{
				await _cabDataServices.DeleteCabAsync(id, currentUserId);
				return Ok(new { Message = "Cab deleted successfully." });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}
	}
}
