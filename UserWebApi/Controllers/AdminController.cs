using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TravelMate.Services;
using UserDll.Models;

namespace TravelMate.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[EnableCors("cors")]
	public class AdminController : ControllerBase
	{
		private readonly IUserDataService UserService;
		private readonly IAdminDataService AdminService;
		public AdminController(IUserDataService userService,IAdminDataService adminService)
		{
			UserService = userService;
			AdminService = adminService;
		}

		[HttpGet]
		[ActionName("GetAllUsers")]
		public async Task<ActionResult<List<User>>> GetAll()
		{
			try
			{
				var users = await AdminService.GetAll();
				return users;
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await AdminService.DeleteUser(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//[HttpGet]
		//[ActionName("GetAdmin")]
		//public async Task<ActionResult<User>> Login(string username, string password)
		//{
		//	try
		//	{
		//		var user = await UserService.Login(username, password);
		//		return user;
		//	}
		//	catch (Exception ex)
		//	{
		//		return NotFound(ex.Message);
		//	}
		//}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] User user)
		{
			try
			{
				await UserService.RegisterUser(user);
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
			try
			{
				await UserService.UpdateUser(user);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
