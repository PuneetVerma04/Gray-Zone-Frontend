using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SteamClone.Frontend.Models;

namespace SteamClone.Frontend.Services;

public interface ICartService
{
    Task<List<CartItemDto>> GetCartAsync();
    Task AddToCartAsync(int gameId);
    Task UpdateQuantityAsync(int gameId, int quantity);
}
public class CartService : ICartService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public CartService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    private async Task SetToken()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<List<CartItemDto>> GetCartAsync()
    {
        await SetToken();
        try
        {
            return await _http.GetFromJsonAsync<List<CartItemDto>>("store/cart") ?? new List<CartItemDto>();
        }
        catch { return new List<CartItemDto>(); }
    }

    public async Task AddToCartAsync(int gameId)
    {
        await SetToken();
        await _http.PostAsJsonAsync("store/cart/add", new { GameId = gameId, Quantity = 1 });
    }

    public async Task UpdateQuantityAsync(int gameId, int quantity)
    {
        await SetToken();
        // Backend expects HTTP PATCH with body
        await _http.PatchAsJsonAsync("store/cart/update", new { GameId = gameId, Quantity = quantity });
    }
}