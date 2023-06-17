using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enumerators;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace EntertenimentManager.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static int GetRequestUserId(this HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            return userId;
        }

        public static bool IsRequestFromAdmin(this HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var userRoles = identity.FindAll(ClaimTypes.Role);
            foreach (var role in userRoles)
            {
                if (role != null &&
                    role.Value == EnumRoles.admin.ToString())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
