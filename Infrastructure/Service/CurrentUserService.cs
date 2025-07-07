using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Service;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var id) ? id : Guid.Empty;
        }
    }

    public Guid StudentId
    {
        get
        {
            var studentIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("studentId")?.Value;
            return Guid.TryParse(studentIdClaim, out var id) ? id : Guid.Empty;
        }
    }
}
