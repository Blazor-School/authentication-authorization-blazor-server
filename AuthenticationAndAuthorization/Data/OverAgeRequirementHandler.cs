using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Data
{
    public class OverAgeRequirementHandler : AuthorizationHandler<OverAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OverAgeRequirement requirement)
        {
            var (value, isSuccess) = TryRetrieveValue(context);
            int minAge = Convert.ToInt32(context?.Resource ?? 0);

            if (isSuccess && value >= minAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }

        private static (int, bool) TryRetrieveValue(AuthorizationHandlerContext context)
        {
            string value = context.User.FindFirst(ct => ct.Type == AdultRequirement.ClaimName)?.Value;

            if (!string.IsNullOrEmpty(value))
            {
                int age = Convert.ToInt32(value);

                return (age, true);
            }
            else
            {
                return (0, false);
            }
        }
    }
}
