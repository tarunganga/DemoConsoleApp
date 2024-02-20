using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace DemoConsoleApp
{
    internal class TokenHandler
    {
        private readonly HttpClient _authClient;
        private readonly string? _clientId;
        private readonly string? _clientSecret;
        private readonly string? _userName;
        private readonly string? _password;

        public TokenHandler(IConfiguration configuration)
        {
            var authUrl = configuration["Authentication:AuthUrl"];
            _clientId = configuration["Authentication:ClientId"];
            _clientSecret = configuration["Authentication:ClientSecret"];
            _userName = configuration["Authentication:UserName"];
            _password = configuration["Authentication:Password"];
            _authClient = new HttpClient();
            _authClient.BaseAddress = new Uri(authUrl);
        }


        public async Task<AuthResponse> GetToken()
        {
            _authClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}")));
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new("grant_type", "password"),
                new("username", _userName),
                new("password", _password)
            });

            var response = await _authClient.PostAsync("/auth/token?scope=truckstop", content);

            return await response.Content.ReadFromJsonAsync<AuthResponse>();


        }
    }
}
