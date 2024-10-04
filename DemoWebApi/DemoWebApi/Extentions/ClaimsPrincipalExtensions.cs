using System.Security.Claims;

namespace DemoWebApi.Extentions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst("id")?.Value;
        }
    }
}
