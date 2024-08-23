using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserDll.Models;

namespace UserDll.Services
{
	public interface IAdminService
	{
		void DeleteUser(int id);
		List<User> GetUsers();
	}

	public class AdminService : IAdminService
	{
		private readonly UserDbContext _context;

		public AdminService(UserDbContext context)
		{
			_context = context;
		}

		public List<User> GetUsers()
		{
			var users = new List<User>();
			users = _context.Users.ToList();
			return users;
		}

		public void DeleteUser(int id)
		{
			try
			{
				var user = _context.Users.FirstOrDefault(u => u.UserId == id);
				if (user != null)
					_context.Users.Remove(user);
				else
					throw new Exception("User not found to delete.");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
	}
}
