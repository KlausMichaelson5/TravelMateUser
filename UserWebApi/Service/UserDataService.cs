using Microsoft.EntityFrameworkCore;
using UserDll.Models;
using UserDll.Services;
using UserWebApi.Models;

namespace TravelMate.Services
{
	public interface IUserDataService
	{
		Task RegisterUser(User user);
		Task UpdateUser(User user);
		Task<User> GetUser(UserLogin user);
	}

	public class UserDataService : IUserDataService
	{
		private readonly IUserService _userService;

		public UserDataService(IUserService service)
		{
			_userService=service;
		}

		public async Task UpdateUser(User user)
		{
			await Task.Run(()=>_userService.UpdateUser(user));
		}

		public async Task RegisterUser(User user)
		{
			await Task.Run(() => _userService.RegisterUser(user));
		}

		public async Task<User> GetUser(UserLogin user)
		{
            return await Task.Run(() => _userService.GetUser(user.UserName,user.Password));
        }

        //public async Task<User> Login(string username,string password)
        //{
        //	var user = await _context.Users
        //		.Where(u => u.Username == username)
        //		.FirstOrDefaultAsync();

        //	if (user == null || password!= user.PasswordHash)
        //	{
        //		throw new Exception("Invalid username or password.");
        //	}
        //	else
        //	{
        //		return user;
        //	}

        //}

        //public Task<User> Logout(User user)
        //{
        //	throw new NotImplementedException();
        //}
    }
}
