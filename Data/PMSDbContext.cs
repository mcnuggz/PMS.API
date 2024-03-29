using Microsoft.EntityFrameworkCore;
using PWS.API.Models;

namespace PWS.API.Data
{
    public class PMSDbContext : DbContext
    {
        public PMSDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
