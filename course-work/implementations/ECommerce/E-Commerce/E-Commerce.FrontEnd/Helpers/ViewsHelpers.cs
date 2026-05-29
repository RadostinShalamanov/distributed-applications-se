using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.FrontEnd.Helpers
{
    public static class ViewsHelpers
    {
        public static string? GetUserRole(this HttpContext context)
        {
            var token = context.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type.EndsWith("role"))?.Value;
            return roleClaim;
        }

        public static bool IsAdmin(this HttpContext context)
        {
            return context.GetUserRole() == "Admin";
        }

        public static string? GetUsername(this HttpContext context)
        {
            var token = context.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return usernameClaim;
        }

    }
}
