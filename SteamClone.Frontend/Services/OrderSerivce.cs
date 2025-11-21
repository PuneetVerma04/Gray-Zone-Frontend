using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace SteamClone.Frontend.Services;

public interface IOrderService
{
    Task<bool> CheckoutAsync();
}

public class OrderService : IOrderService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public OrderService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<bool> CheckoutAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrEmpty(token)) return false;

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Backend endpoint: POST /store/order/checkout
        var response = await _http.PostAsync("store/order/checkout", null);
        return response.IsSuccessStatusCode;
    }
}