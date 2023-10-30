using System.Security.Claims;
using BookPublisher.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BookPublisher.Application.Services;

public class UserLoggedService : IUserLoggedService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserLoggedService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor; 
    }

    public long GetCurrentUserId()
    {
        var id = long.Parse(_httpContextAccessor.HttpContext?.User.
            FindFirstValue(ClaimTypes.NameIdentifier));

        return id; 
    }
}
