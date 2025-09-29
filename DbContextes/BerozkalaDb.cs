using berozkala_backend.Entities.AccountsEntities;
using berozkala_backend.Entities.OrderEntities;
using berozkala_backend.Entities.OtherEntities;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.DbContextes
{
    public class BerozkalaDb : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminAccount> Admins { get; set; }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<PeymentMethod> PeymentMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<ProductCategory> ProductCategorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=DbFiles\BerozkalaDb.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(u =>
            {
                u.HasIndex(e => e.UserName).IsUnique();
                u.HasIndex(e => e.PhoneNumber).IsUnique();
                u.HasIndex(e => e.Email).IsUnique();
                u.Property(e => e.UserName).HasMaxLength(128);
            });

            modelBuilder.Entity<AdminAccount>(u =>
            {
                u.HasIndex(e => e.UserName).IsUnique();
                u.HasIndex(e => e.PhoneNumber).IsUnique();
                u.HasIndex(e => e.Email).IsUnique();
                u.Property(e => e.UserName).HasMaxLength(128);
            });
        }
    }
}