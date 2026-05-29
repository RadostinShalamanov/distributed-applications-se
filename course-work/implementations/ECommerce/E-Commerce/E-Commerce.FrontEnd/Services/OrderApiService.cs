using E_Commerce.FrontEnd.Models;
using E_Commerce.FrontEnd.Models.Orders;
using E_Commerce.FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using E_Commerce.FrontEnd.Models.Common;

namespace E_Commerce.FrontEnd.Services
{
    public class OrderApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<PagedResponse<OrderResponseModel>> GetOrders(string query = "api/orders")
        {
            var req = new HttpRequestMessage(HttpMethod.Get, query);
            SetAuthorization(req);
            var resp = await _httpClient.SendAsync(req);
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PagedResponse<OrderResponseModel>>();
        }

        public async Task<OrderResponseModel> GetOrderById(int id)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"api/orders/{id}");
            SetAuthorization(req);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<OrderResponseModel>();
        }

        public async Task CreateOrder(OrderRequestModel dto, string? token = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, "api/orders") { Content = JsonContent.Create(dto) };
            SetAuthorization(req);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateOrder(int id, OrderUpdateModel dto, string? token = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Patch, $"api/orders/{id}/status") { Content = JsonContent.Create(dto) };
            SetAuthorization(req, token);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrder(int id, string? token = null)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, $"api/orders/{id}");
            SetAuthorization(req);
            var resp = await _httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<bool> PayOrder(int orderId, decimal amount)
        {
            var req = new HttpRequestMessage(HttpMethod.Patch, $"api/orders/{orderId}/pay")
            {
                Content = JsonContent.Create(new { Amount = amount })
            };

            SetAuthorization(req);

            var resp = await _httpClient.SendAsync(req);

            return resp.IsSuccessStatusCode;

        }

        private string? GetToken() => _httpContextAccessor.HttpContext?.Session.GetString("JWToken");

        private void SetAuthorization(HttpRequestMessage req, string? providedToken = null)
        {
           
            var token = !string.IsNullOrWhiteSpace(providedToken) ? providedToken : GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
