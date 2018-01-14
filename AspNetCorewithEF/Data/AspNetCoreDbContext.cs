using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorewithEF.Data
{
    public class AspNetCoreDbContext : DbContext
    {
        public AspNetCoreDbContext (DbContextOptions<AspNetCoreDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=YOUR_HOST;Database=YOUR_DATABASE;User id=YOUR_USERID;Password=YOUR_PASSWORD;Integrated Security=false;MultipleActiveResultSets=true");
        }

        public DbSet<AspNetCorewithEF.Models.Employees> Employees { get; set; }
    }
}
