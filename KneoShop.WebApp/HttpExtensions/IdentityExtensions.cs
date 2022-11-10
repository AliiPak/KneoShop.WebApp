using System.Security.Claims;

namespace KenoShop.WebApp.HttpExtensions;

public static class IdentityExtensions
{
    public static int GetCurrentUserId(this ClaimsPrincipal principal)
    {
        if (principal.Identity.IsAuthenticated)
        {
            return Convert.ToInt32(principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        return 0;
    }

    public static string GetCurrentUserEmail(this ClaimsPrincipal principal)
    {
        if (principal.Identity.IsAuthenticated)
        {
            return principal.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        }

        return "";
    }
}