using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelMate.Services;
using UserDll.Models;
using UserWebApi.Models;

namespace TravelMate.Controllers
{
	[Route("api/users")]
    [ApiController]
	[EnableCors("cors")]
	public class UserController : ControllerBase
	{
		private readonly IUserDataService _service;

		public UserController(IUserDataService userService)
		{
			_service = userService;
		}

		[HttpPost("login")]
        [Authorize]
        public async Task<ActionResult<User>> Get(UserLogin user)
		{
			try
			{
				var tempUser = await _service.GetUser(user);
				return Ok(tempUser);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] User user)
		{
			try
			{
				await _service.RegisterUser(user);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                await _service.UpdateUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
