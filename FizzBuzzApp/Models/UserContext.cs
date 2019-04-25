using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzApp.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            //this.Users.RemoveRange(this.Users);
        }

        public DbSet<User> Users { get; set; }
    }
}
