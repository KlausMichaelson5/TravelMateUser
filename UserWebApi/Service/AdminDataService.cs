using Microsoft.EntityFrameworkCore;
using UserDll.Models;
using UserDll.Services;

namespace TravelMate.Services
{
	public interface IAdminDataService
	{
		Task<List<User>> GetAll();
		Task DeleteUser(int id);
	}
	public class AdminDataService : IAdminDataService
	{
		private readonly IAdminService _adminService;

		public AdminDataService(IAdminService service)
		{
			_adminService = service;
		}

		public async Task DeleteUser(int id)
		{
			await Task.Run(() => _adminService.DeleteUser(id));

		}

		public async Task<List<User>> GetAll()
		{
			return await Task.Run(() => _adminService.GetUsers());
		}
	}
}
