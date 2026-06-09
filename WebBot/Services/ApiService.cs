using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebBot.Models;

namespace WebBot.Services;

public class ApiService
{
    private readonly HttpClient _http;
    private readonly AuthState _auth;

    public ApiService(HttpClient http, AuthState auth)
    {
        _http = http;
        _auth = auth;
    }

    private void SetAuth()
    {
        var token = _auth.Token;
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        else
            _http.DefaultRequestHeaders.Authorization = null;
    }

    // --- Store Types ---
    public async Task<List<StoreType>> GetStoreTypesAsync()
    {
        return await _http.GetFromJsonAsync<List<StoreType>>("store-types") ?? new();
    }

    // --- Stores ---
    public async Task<List<Store>> GetStoresAsync(Guid storeTypeId)
    {
        return await _http.GetFromJsonAsync<List<Store>>($"stores?storeTypeId={storeTypeId}") ?? new();
    }

    // --- Categories ---
    public async Task<List<Category>> GetCategoriesAsync(Guid storeTypeId)
    {
        return await _http.GetFromJsonAsync<List<Category>>($"categories?storeTypeId={storeTypeId}") ?? new();
    }

    // --- Products ---
    public async Task<List<Product>> GetProductsAsync(Guid storeId, Guid? categoryId = null)
    {
        var url = $"products?storeId={storeId}";
        if (categoryId.HasValue) url += $"&categoryId={categoryId.Value}";
        return await _http.GetFromJsonAsync<List<Product>>(url) ?? new();
    }

    public async Task<Product?> GetProductAsync(Guid productId)
    {
        return await _http.GetFromJsonAsync<Product>($"products/{productId}");
    }

    // --- Auth ---
    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var resp = await _http.PostAsJsonAsync("auth/login", request);
        if (!resp.IsSuccessStatusCode) return null;
        var result = await resp.Content.ReadFromJsonAsync<AuthResponse>();
        if (result != null) _auth.Set(result);
        return result;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
    {
        var resp = await _http.PostAsJsonAsync("auth/register", request);
        if (!resp.IsSuccessStatusCode) return null;
        var result = await resp.Content.ReadFromJsonAsync<AuthResponse>();
        if (result != null) _auth.Set(result);
        return result;
    }

    // --- Orders (Client) ---
    public async Task<Order?> CreateOrderAsync(CreateOrderRequest request)
    {
        SetAuth();
        var resp = await _http.PostAsJsonAsync("orders", request);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<Order>();
    }

    public async Task<List<Order>> GetMyOrdersAsync()
    {
        SetAuth();
        return await _http.GetFromJsonAsync<List<Order>>("orders/my") ?? new();
    }

    // --- Courier ---
    public async Task<CourierProfile?> GetCourierProfileAsync()
    {
        SetAuth();
        return await _http.GetFromJsonAsync<CourierProfile>("couriers/profile");
    }

    public async Task<bool> UpdateCourierProfileAsync(CourierProfile profile)
    {
        SetAuth();
        var resp = await _http.PutAsJsonAsync("couriers/profile", profile);
        return resp.IsSuccessStatusCode;
    }

    public async Task<List<Order>> GetAvailableOrdersAsync()
    {
        SetAuth();
        return await _http.GetFromJsonAsync<List<Order>>("orders/courier/available") ?? new();
    }

    public async Task<List<Order>> GetCourierOrdersAsync()
    {
        SetAuth();
        return await _http.GetFromJsonAsync<List<Order>>("orders/courier/my") ?? new();
    }

    public async Task<List<Order>> GetCourierHistoryAsync()
    {
        SetAuth();
        return await _http.GetFromJsonAsync<List<Order>>("orders/courier/history") ?? new();
    }

    public async Task<bool> AcceptOrderAsync(Guid orderId)
    {
        SetAuth();
        var resp = await _http.PatchAsync($"orders/{orderId}/accept", null);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeliverOrderAsync(Guid orderId)
    {
        SetAuth();
        var resp = await _http.PatchAsync($"orders/{orderId}/deliver", null);
        return resp.IsSuccessStatusCode;
    }
}
