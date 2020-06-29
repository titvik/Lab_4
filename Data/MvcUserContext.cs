using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Data
{
    public class MvcUserContext : DbContext
    {
        public MvcUserContext(DbContextOptions<MvcUserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}
