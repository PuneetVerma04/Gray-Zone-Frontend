using System.Net.Http.Json;
using SteamClone.Frontend.Models;

namespace SteamClone.Frontend.Services;

public interface ICatalogService
{
    Task<List<GameResponseDTO>> GetGamesAsync();
}

public class CatalogService : ICatalogService
{
    private readonly HttpClient _http;

    public CatalogService(HttpClient http)
    {
        _http = http;
    }

    // Backend endpoint: GET /store/games
    public async Task<List<GameResponseDTO>> GetGamesAsync()
    {
        // Using "all" or specific query params based on your backend
        var result = await _http.GetFromJsonAsync<PagedGameResponse>("store/games?PageSize=50");
        return result?.Games.ToList() ?? new List<GameResponseDTO>();
    }

    // Helper class for pagination response
    private class PagedGameResponse
    {
        public IEnumerable<GameResponseDTO> Games { get; set; } = Enumerable.Empty<GameResponseDTO>();
    }
}