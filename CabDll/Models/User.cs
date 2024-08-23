using System.ComponentModel.DataAnnotations;

namespace TravelMate.Models
{
	public enum UserType
	{
		User,
		Hotel_Owner,
		Driver_Client,
		Admin
	}
	public enum AuthProvider
	{
		Local,
		Google
	}

	public enum Nationality
	{
		IND,
		USA,
		UK,
		RUS,
		JPN
	}
	public class User
	{
		[Key]
		public int UserId { get; set; }

		[Required]
		public string Username { get; set; }=string.Empty;

		[Required]//If you are using google not required.
		public string PasswordHash { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public Nationality Nationality { get; set; }

		[Required]
		public string Email { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public AuthProvider AuthProvider { get; set; }=AuthProvider.Local;

		public UserType UserType { get; set; }
	}
}
