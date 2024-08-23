using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDll.Models;

namespace UserDll.Services
{
	public interface IUserService
	{
		void RegisterUser(User user);
		void UpdateUser(User user);
		User GetUser(string username,string password);
	}

	public class UserService : IUserService
	{
		private readonly UserDbContext _context;

		public UserService(UserDbContext context)
		{
			_context = context;
		}

        public User GetUser(string username, string password)
        {
            var tempUser = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash==password);
			return tempUser;//add null check

        }

        public void RegisterUser(User user)
		{
			_context.Users.Add(user);
			_context.SaveChanges();
		}

		public void UpdateUser(User user)
		{
			var tempUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
			if (tempUser != null)
			{
				tempUser.Username = user.Username;
				tempUser.PhoneNumber = user.PhoneNumber;
				tempUser.Name = user.Name;
				tempUser.Email = user.Email;
				tempUser.Address = user.Address;
				tempUser.AuthProvider = user.AuthProvider;
				tempUser.PasswordHash = user.PasswordHash;
				tempUser.Nationality = user.Nationality;
				tempUser.UserType = user.UserType;
				_context.SaveChanges();

			}
		}
	}
}
