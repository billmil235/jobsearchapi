using System.Security.Claims;

namespace JobSearch.Extensions;

public static class UserExtensions
{
    public static Guid GetGuid(this ClaimsPrincipal user)
    {
        var guid = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userId = new Guid(guid);
        return userId;
    }
}