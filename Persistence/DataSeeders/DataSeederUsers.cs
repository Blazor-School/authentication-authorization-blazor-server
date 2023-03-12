using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataSeeders
{
    public class DataSeederUsers
    {
        public static ModelBuilder SeedData(ModelBuilder modelBuilder)
        {
            List<User> Users = new()
            {
                new()
                {
                    Id = 1,
                    Username = "User1",
                    Password = "User1",
                    Age = 19,
                    IsPremiumMember = false
                },
                new()
                {
                    Id = 2,
                    Username = "User2",
                    Password = "User2",
                    Age = 10,
                    IsPremiumMember = true
                },
                new()
                {
                    Id = 3,
                    Username = "User3",
                    Password = "User3",
                    Age = 20,
                    IsPremiumMember = true
                }
            };

            List<Toy> Toys = new()
            {
                new()
                {
                    Id = 1,
                    Name = "Car",
                    AgeRequired = 0
                },
                new()
                {
                    Id = 2,
                    Name = "Plastic knife",
                    AgeRequired = 15
                }
            };

            modelBuilder.Entity<User>().HasData(Users);
            modelBuilder.Entity<Toy>().HasData(Toys);

            return modelBuilder;
        }
    }
}