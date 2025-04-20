using CustomerJobApp.Models;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using CustomerJobApp.Models;

namespace CustomerJobApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<CustomerJob> CustomerJobs { get; set; }
    }
}
