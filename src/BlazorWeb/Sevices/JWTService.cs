using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace BlazorWeb.Sevices;

public class JWTService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILocalStorageService _localStorage;

    public JWTService(IJSRuntime jsRuntime, ILocalStorageService localStorage)
    {
        _jsRuntime = jsRuntime;
        _localStorage = localStorage;
    }

    public async Task<string> GetCurrentUserId()
    {
        var tokentest = await _localStorage.GetItemAsStringAsync("authToken");

        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (string.IsNullOrWhiteSpace(token))
            return null;

        token = token.Trim('\"');
        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(token);

        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        return userId;
    }
}
