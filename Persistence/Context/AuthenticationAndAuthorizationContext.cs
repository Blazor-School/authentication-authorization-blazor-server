using Microsoft.EntityFrameworkCore;
using Persistence.DataSeeders;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class AuthenticationAndAuthorizationContext : DbContext, IAuthenticationAndAuthorizationContext
    {
        public DbSet<Toy> Toys { get; set; }

        public DbSet<User> Users { get; set; }

        public AuthenticationAndAuthorizationContext(DbContextOptions<AuthenticationAndAuthorizationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = DataSeederUsers.SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
