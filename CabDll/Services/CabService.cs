using CabDll.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CabDll.Services
{
	public interface ICabService
	{
		void Add(Cab cab, int currentUserId);
		void Delete(int id, int currentUserId);
		Cab Get(int currentUserId);
		void Update(Cab cab, int currentUserId);
	}

	public class CabService : ICabService
	{
		private readonly CabDbContext _context;
		//private readonly IHttpContextAccessor _httpContextAccessor;

		public CabService(CabDbContext context)
		{
			_context = context;
		}

		public Cab Get(int currentUserId)
		{
			var cabEntity = _context.Cabs.FirstOrDefault(c => c.DriverId == currentUserId);

			if (cabEntity == null)
			{
				throw new Exception("No cab found for the current user.");
			}

			return new Cab
			{
				CabId = cabEntity.CabId,
				DriverId = cabEntity.DriverId,
				VehicleName = cabEntity.VehicleName,
				RegistrationNumber = cabEntity.RegistrationNumber,
				LicenseNumber = cabEntity.LicenseNumber,
				CabPhoto = cabEntity.CabPhoto,
				DriverPhoto = cabEntity.DriverPhoto,
				Latitude = cabEntity.Latitude,
				Longitude = cabEntity.Longitude,
				AvailabilityStatus = cabEntity.AvailabilityStatus,
				VehicleType = cabEntity.VehicleType,
				NumberOfSeats = cabEntity.NumberOfSeats,
				PricePerKm = cabEntity.PricePerKm,
				Rating = cabEntity.Rating
			};
		}

		public void Add(Cab cab, int currentUserId)
		{

			var cabEntity = new Cab
			{
				DriverId = currentUserId,//need to add compare
				VehicleName = cab.VehicleName,
				RegistrationNumber = cab.RegistrationNumber,
				LicenseNumber = cab.LicenseNumber,
				CabPhoto = cab.CabPhoto,
				DriverPhoto = cab.DriverPhoto,
				Latitude = cab.Latitude,
				Longitude = cab.Longitude,
				AvailabilityStatus = cab.AvailabilityStatus,
				VehicleType = cab.VehicleType,
				NumberOfSeats = cab.NumberOfSeats,
				PricePerKm = cab.PricePerKm,
				Rating = cab.Rating
			};

			_context.Cabs.AddAsync(cabEntity);
			_context.SaveChangesAsync();
		}

		public void Update(Cab cab, int currentUserId)
		{
			var cabEntity = _context.Cabs.Find(cab.CabId);

			if (cabEntity == null || cabEntity.DriverId != currentUserId)
			{
				throw new Exception("Cab not found or you do not have access to update.");
			}

			cabEntity.VehicleName = cab.VehicleName;
			cabEntity.RegistrationNumber = cab.RegistrationNumber;
			cabEntity.LicenseNumber = cab.LicenseNumber;
			cabEntity.CabPhoto = cab.CabPhoto;
			cabEntity.DriverPhoto = cab.DriverPhoto;
			cabEntity.Latitude = cab.Latitude;
			cabEntity.Longitude = cab.Longitude;
			cabEntity.AvailabilityStatus = cab.AvailabilityStatus;
			cabEntity.VehicleType = cab.VehicleType;
			cabEntity.NumberOfSeats = cab.NumberOfSeats;
			cabEntity.PricePerKm = cab.PricePerKm;
			cabEntity.Rating = cab.Rating;

			_context.SaveChangesAsync();
		}

		public void Delete(int id, int currentUserId)
		{

			var cabEntity = _context.Cabs.Find(id);

			if (cabEntity == null || cabEntity.DriverId != currentUserId)
			{
				throw new Exception("Cab not found or you do not have access to delete.");
			}

			_context.Cabs.Remove(cabEntity);
			_context.SaveChangesAsync();
		}
	}
}