using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TravelMate.Models;
using TravelMateUI;

namespace TravelMate2.Services
{
	public class UserInfoService()
	{
        public int UserId { get; set; }
        public User? _currentUser;

        // Method to set the current user
        public void SetCurrentUser(User user)
        {
            _currentUser = user;
            UserId = user.UserId;
        }

        // Method to get the current user
        public User? GetCurrentUser()
        {
            return _currentUser;
        }

        // Method to clear user information on logout
        public void ClearUser()
        {
            _currentUser = null;
            UserId = 0;
        }
    }
	public class AuthService
	{

		private readonly HttpClient http;
		public AuthService()
		{
			http = new HttpClient();
			http.BaseAddress = new Uri(Program.Configuration["AuthUrl"]!);

		}

		public async Task<string> GetJwtToken(UserInfo user)
		{
			var json = JsonSerializer.Serialize(user);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await http.PostAsync("", content);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}
	}
	public interface IUserUIService
    {
        Task Add(User user);
		//Task<User> Login(string username,string password);
        Task<User> GetUser(UserInfo user);
		Task UpdateUser(int id,User user);
    }
    public class UserUIService : IUserUIService
    {
        private readonly HttpClient httpClient;
		private readonly AuthService _authService;
		public UserUIService(HttpClient client, AuthService authService)
        {
            this.httpClient = client;
            _authService = authService;
        }
        public async Task Add(User user)
        {
             await httpClient.PostAsJsonAsync<User>("users/", user);
            
        }

		public async Task<User> GetUser(UserInfo user)
		{
			var token = await _authService.GetJwtToken(new UserInfo { Username = user.Username, Password = user.Password });
			Console.WriteLine(token);
			var data = JsonSerializer.Deserialize<TokenInfo>(token);
			Console.WriteLine(data.token);


            httpClient.BaseAddress = new Uri("http://localhost:5254/api/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.token);

            var response = await httpClient.PostAsJsonAsync($"users/login",user);
			if (response.IsSuccessStatusCode)
			{
				var authenticatedUser = await response.Content.ReadFromJsonAsync<User>();
				return authenticatedUser;
			}
			else
			{
				throw new Exception("Invalid Credentials");
			}
		}

        public async Task UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                throw new ArgumentException("User ID Miss Matched");
            }
            var response = await httpClient.PutAsJsonAsync($"users/{id}",user);
            if(!response.IsSuccessStatusCode)
            {
                var errorMessage= await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }

        //public async Task<User> Login(string username,string password)
        //      {
        //	var token = await _authService.GetJwtToken(new UserInfo { Username = "test", Password = "password" });
        //	Console.WriteLine(token);
        //	var data = JsonSerializer.Deserialize<TokenInfo>(token);
        //	Console.WriteLine(data.token);
        //	var http = HttpClientFactory.CreateHttp("http://localhost:5254/api/Auth/login", data.token);
        //	var response = await http.GetAsync($"users/?username={username}&password={password}");
        //          if(response.IsSuccessStatusCode)
        //          {
        //              var authenticatedUser=await response.Content.ReadFromJsonAsync<User>();
        //              return authenticatedUser;
        //          }
        //          else
        //          {
        //              throw new Exception("Invalid Credentials");
        //          }
        //      }
    }
}


