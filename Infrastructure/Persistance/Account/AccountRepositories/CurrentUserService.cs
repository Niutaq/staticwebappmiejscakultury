using System.Security.Claims;
using Application.Persistance.Interfaces.AccountInterfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistance.Account.AccountRepositories;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid UserId => GetClaimAsGuid(ClaimTypes.NameIdentifier, _httpContextAccessor);
    public IList<string> UserRoles() => _httpContextAccessor.HttpContext.User.Claims
        .Where(x => x.Type == ClaimTypes.Role)
        .Select(x => x.Value)
        .ToList();
    

    private static Guid GetClaimAsGuid(string claimIdentifier, IHttpContextAccessor contextAccessor)
    {
        var claim = contextAccessor?.HttpContext?.User.FindFirstValue(claimIdentifier);

        if (string.IsNullOrWhiteSpace(claim))
        {
            return Guid.Empty;
        }

        var parseGuidClaim = Guid.TryParse(claim, out var claimId);

        if (!parseGuidClaim || claimId == Guid.Empty)
        {
            return Guid.Empty;
        }

        return claimId;
    }
}