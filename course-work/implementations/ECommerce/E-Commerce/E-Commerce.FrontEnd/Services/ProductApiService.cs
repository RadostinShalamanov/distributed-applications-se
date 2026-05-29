using E_Commerce.FrontEnd.Models.Common;
using E_Commerce.FrontEnd.Models.Products;
using E_Commerce.FrontEnd.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace E_Commerce.FrontEnd.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetToken() => _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

        public async Task<PagedResponse<ProductResponseModel>?> GetProducts(string query = "api/products")
        {
            var req = new HttpRequestMessage(HttpMethod.Get, query);

            var token = GetToken();
            if (!string.IsNullOrWhiteSpace(token)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var resp = await _httpClient.SendAsync(req);
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            
            
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PagedResponse<ProductResponseModel>>();
        }

        public async Task<ProductResponseModel?> GetProductById(int id)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"api/products/{id}");
            var token = GetToken();
            if (!string.IsNullOrWhiteSpace(token)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = await _httpClient.SendAsync(req);
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<ProductResponseModel>();
        }

        public async Task CreateProduct(ProductRequestModel dto, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, "api/products")
            {
                Content = JsonContent.Create(dto)
            };
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);

            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateProduct(
            int id,
            ProductUpdateModel dto,
            string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Put, $"api/products/{id}") { Content = JsonContent.Create(dto) };
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);

            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteProduct(int id, string token)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, $"api/products/{id}");
            var t = token ?? GetToken();
            if (!string.IsNullOrWhiteSpace(t)) req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", t);

            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }
    }
}
