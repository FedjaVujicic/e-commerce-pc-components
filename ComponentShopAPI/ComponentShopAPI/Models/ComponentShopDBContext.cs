using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Models
{
    public class ComponentShopDBContext : IdentityDbContext
    {
        public ComponentShopDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Gpu> Gpus { get; set; }
    }
}
