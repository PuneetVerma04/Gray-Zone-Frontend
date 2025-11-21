using Blazored.LocalStorage;
using System.Net.Http.Json;
using SteamClone.Frontend.Models; // Add using for your models
using SteamClone.Frontend.Auth; // For AuthStateProvider
using Microsoft.AspNetCore.Components.Authorization;

namespace SteamClone.Frontend.Services;

public interface IAuthService
{
    Task<bool> Login(LoginDto loginModel);
    Task Logout();
}
public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> Login(LoginDto loginModel)
    {
        var response = await _http.PostAsJsonAsync("store/auth/login", loginModel);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            await _localStorage.SetItemAsync("authToken", result!.Token);
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
            return true;
        }
        return false;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}