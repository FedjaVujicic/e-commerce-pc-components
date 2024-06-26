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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Products)
                .WithMany()
                .UsingEntity<CartProduct>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Gpu> Gpus { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProduct { get; set; }
    }
}
