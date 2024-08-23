using System.ComponentModel.DataAnnotations;

namespace TravelMate.Models
{
    public enum UserType
    {
        Regular,
        hotel_owner,
        driver_client
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
	public class TokenInfo
	{
		public string token { get; set; } = string.Empty;
	}
	public class UserInfo()
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]//If you are using google not required.
        public string PasswordHash { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Nationality Nationality { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public AuthProvider AuthProvider { get; set; } = AuthProvider.Local;

        public UserType UserType { get; set; }
    }
	//public class HttpClientFactory
	//{
	//	public static HttpClient CreateHttp(string baseAddres, string token)
	//	{
	//		var httpClient = new HttpClient();
	//		httpClient.BaseAddress = new Uri(baseAddres);
	//		httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
	//		return httpClient;
	//	}
	//}
}