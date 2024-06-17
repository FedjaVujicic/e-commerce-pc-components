using ComponentShopAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Monitor = ComponentShopAPI.Entities.Monitor;

namespace ComponentShopAPI.Repositories
{
    public class ComponentShopDBContext : IdentityDbContext<ApplicationUser>
    {
        public ComponentShopDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Gpu> Gpus { get; set; }
    }
}
