using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public interface IAuthenticationAndAuthorizationContext
    {
        DbSet<Toy> Toys { get; set; }

        DbSet<User> Users { get; set; }
    }
}
