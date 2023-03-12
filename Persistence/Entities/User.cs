using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int Age { get; set; }

        public bool IsPremiumMember { get; set; }
    }
}
