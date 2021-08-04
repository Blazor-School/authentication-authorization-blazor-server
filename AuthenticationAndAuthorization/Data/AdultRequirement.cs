using Microsoft.AspNetCore.Authorization;

namespace AuthenticationAndAuthorization.Data
{
    public class AdultRequirement : IAuthorizationRequirement
    {
        public static string ClaimName => "Age";
    }
}