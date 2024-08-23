using CabDll.Models;
using CabDll.Services;
using System.Threading.Tasks;

namespace TravelMate.DataService
{
	public class CabDataServices
	{
		private readonly ICabService _cabService;

		public CabDataServices(ICabService cabService)
		{
			_cabService = cabService;
		}

		public  Task<Cab> GetCabAsync(int currentUserId)
		{
			return  Task.Run(() => _cabService.Get(currentUserId));
		}

		public  Task AddCabAsync(Cab cab, int currentUserId)
		{
			 return Task.Run(() => _cabService.Add(cab, currentUserId));
		}

		public  Task UpdateCabAsync(Cab cab, int currentUserId)
		{
			return Task.Run(() => _cabService.Update(cab, currentUserId));
		}

		public  Task DeleteCabAsync(int id, int currentUserId)
		{
			return Task.Run(() => _cabService.Delete(id, currentUserId));
		}
	}
}
