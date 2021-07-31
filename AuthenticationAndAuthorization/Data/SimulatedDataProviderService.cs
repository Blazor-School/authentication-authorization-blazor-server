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
                Age = 19
            },
            new()
            {
                Username = "User2",
                Password = "User2",
                Age = 10
            }
        };
    }
}
