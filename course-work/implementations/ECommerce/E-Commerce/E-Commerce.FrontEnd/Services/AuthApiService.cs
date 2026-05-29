using System.Net.Http.Json;
using System.Text.Json;

namespace E_Commerce.FrontEnd.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Calls backend login endpoint and returns JWT string if successful.
        /// Expects a JSON response with nested structure: { data: { token: "..." } }
        /// </summary>
        public async Task<string?> Login(string email, string password)
        {
            var payload = new { email, password };
            var resp = await _httpClient.PostAsJsonAsync("api/auth/login", payload);
            if (!resp.IsSuccessStatusCode)
                return null;

            var content = await resp.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(content);
                var root = doc.RootElement;

                // Try nested structure: data.token
                if (root.TryGetProperty("data", out var data))
                {
                    if (data.TryGetProperty("token", out var t)) return t.GetString();
                }

                // Try top-level token properties
                if (root.TryGetProperty("token", out var token)) return token.GetString();
                if (root.TryGetProperty("accessToken", out var accessToken)) return accessToken.GetString();
                if (root.TryGetProperty("jwt", out var jwt)) return jwt.GetString();
            }
            catch
            {
                // Not valid JSON, might be a raw token string
            }

            // If response is a raw string (JWT token)
            if (!string.IsNullOrWhiteSpace(content))
            {
                var trimmed = content.Trim('"', ' ', '\n', '\r');
                // Validate it looks like a JWT (has dots separating parts)
                if (trimmed.Contains('.'))
                {
                    return trimmed;
                }
            }

            return null;
        }

        /// <summary>
        /// Calls backend register endpoint to create a new user.
        /// </summary>
        public async Task<string> Register(string username, string email, string password, string confirmPassword)
        {
            var payload = new
            {
                username,
                email,
                password,
                confirmPassword
            };

            var resp = await _httpClient.PostAsJsonAsync("api/auth/register", payload);

            if (!resp.IsSuccessStatusCode)
                return null;

            var content = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);

            return doc.RootElement
                      .GetProperty("data")
                      .GetProperty("token")
                      .GetString();
        }
    }
}
