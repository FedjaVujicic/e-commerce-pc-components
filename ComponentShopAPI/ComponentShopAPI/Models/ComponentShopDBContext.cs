using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Models
{
    public class ComponentShopDBContext : DbContext
    {
        public ComponentShopDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Gpu> GraphicsCards { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
