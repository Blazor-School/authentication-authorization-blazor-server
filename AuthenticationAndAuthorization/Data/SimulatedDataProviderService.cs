using System.Collections.Generic;
using Persistence.Entities;

namespace AuthenticationAndAuthorization.Data
{
    public class SimulatedDataProviderService
    {
        public IEnumerable<string> GetUserRoles(User user)
        {
            var result = user.Username switch
            {
                "User1" => new List<string>() { "Admin", "User" },
                "User2" => new List<string>() { "User" },
                _ => new List<string>() { },
            };

            return result;
        }
    }
}
