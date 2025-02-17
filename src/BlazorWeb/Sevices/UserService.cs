using System.Net.Http;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using BlazorWeb.Shared.Models;

namespace BlazorWeb.Sevices;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    public UserService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
        _localStorage = localStorage;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/user/GetAll");
    }
}
