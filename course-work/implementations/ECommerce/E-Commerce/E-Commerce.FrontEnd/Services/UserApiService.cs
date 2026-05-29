using E_Commerce.FrontEnd.Models;
using E_Commerce.FrontEnd.Models.Users;
using E_Commerce.FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using E_Commerce.FrontEnd.Models.Common;

namespace E_Commerce.FrontEnd.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetToken() => _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

        public async Task<PagedResponse<UserResponseModel>> GetUsers(string query = "api/users")
        {
            var req = new HttpRequestMessage(HttpMethod.Get, query);
            var token = GetToken();
            if (!string.IsNullOrWhiteSpace(token)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = await _httpClient.SendAsync(req);
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PagedResponse<UserResponseModel>>();
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"api/users/{id}");
            var token = GetToken();
            if (!string.IsNullOrWhiteSpace(token)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<UserResponseModel>();
        }

        public async Task CreateUser(UserRequestModel dto, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, "api/users") { Content = JsonContent.Create(dto) };
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateUser(int id, UserUpdateModel dto, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, $"api/users/{id}") { Content = JsonContent.Create(dto) };
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteUser(int id, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, $"api/users/{id}");
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }
    }
}
