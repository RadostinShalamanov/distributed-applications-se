using E_Commerce.FrontEnd.Models;
using E_Commerce.FrontEnd.Models.Categories;
using E_Commerce.FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using E_Commerce.FrontEnd.Models.Common;

namespace E_Commerce.FrontEnd.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetToken() => _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

        public async Task<PagedResponse<CategoryResponseModel>> GetCategories(string query = "api/categories")
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "api/categories");
            var token = GetToken();
            if (!string.IsNullOrWhiteSpace(token)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var resp = await _httpClient.SendAsync(req);
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PagedResponse<CategoryResponseModel>>();
        }

       

        public async Task CreateCategory(CategoryRequestModel dto, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, "api/categories") { Content = JsonContent.Create(dto) };
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateCategory(int id, CategoryUpdateModel dto, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, $"api/categories/{id}") { Content = JsonContent.Create(dto) };
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteCategory(int id, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, $"api/categories/{id}");
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }
    }
}
