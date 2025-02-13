using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using oig_assessment.Application.Interfaces;

namespace oig_assessment.Infrastructure.Identity;
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;

            if (Guid.TryParse(userIdClaim, out var parsedUserId)) 
            {
                return parsedUserId;
            }

            throw new UnauthorizedAccessException("UserId not found or uncorrect.");
        }
    }
        
    public string? Role => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
}

