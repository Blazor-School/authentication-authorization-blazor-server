using System.Collections.Generic;

namespace AuthenticationAndAuthorization.Data
{
    public class SimulatedDataProviderService
    {
        public List<User> Users { get; set; } = new()
        {
            new()
            {
                Username = "User1",
                Password = "User1",
                Age = 19,
                IsPremiumMember = false
            },
            new()
            {
                Username = "User2",
                Password = "User2",
                Age = 10,
                IsPremiumMember = true
            },
            new()
            {
                Username = "User3",
                Password = "User3",
                Age = 20,
                IsPremiumMember = true
            }
        };

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
