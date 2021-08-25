using Microsoft.AspNetCore.Authorization;

namespace AuthenticationAndAuthorization.Data
{
    public class OverAgeRequirement: IAuthorizationRequirement
    {
        public static string ClaimName => "Age";
    }
}
