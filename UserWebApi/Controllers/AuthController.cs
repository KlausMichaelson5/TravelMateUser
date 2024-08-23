using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserWebApi.Models;
using UserDll.Models;
using TravelMate.Services;

namespace UserWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors("cors")]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IUserDataService _usedDataSerice;
		private const string privateKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		public AuthController(IConfiguration configuration,IUserDataService userDataService)
		{
			_configuration = configuration;
			_usedDataSerice = userDataService;
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] UserLogin user)
		{
			// Validate the user credentials (this is just a dummy check, replace with your logic)
			var tempUser = _usedDataSerice.GetUser(user);
			if (tempUser!=null && user.Password == tempUser.Result.PasswordHash)
			{
				var token = GenerateJwtToken(user.UserName);
				return Ok(new { token });
			}

			return Unauthorized();
		}
		private string GenerateJwtToken(string username)
		{
			var key = Encoding.UTF8.GetBytes(privateKey);
			var tokenDesc = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, username)
			}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDesc);
			return tokenHandler.WriteToken(token);
		}
	}
	}
