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
        public DbSet<Category> Categorys { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductGarranty> ProductGarrantys { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AttributeSubset> ProductSubsetAttributes { get; set; }
        public DbSet<ProductsSubCategorys> ProductsSubCategorys { get; set; }

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

            modelBuilder.Entity<AdminAccount>(a =>
            {
                a.HasIndex(e => e.UserName).IsUnique();
                a.HasIndex(e => e.PhoneNumber).IsUnique();
                a.HasIndex(e => e.Email).IsUnique();
                a.Property(e => e.UserName).HasMaxLength(128);
            });

            modelBuilder.Entity<Category>()
                .HasMany(x => x.SubCategorys)
                .WithOne(x => x.ProductCategory)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductAttribute>()
                .HasMany(x => x.Subsets)
                .WithOne(x => x.Attribute)
                .HasForeignKey(x => x.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.ImagesUrls)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.Attributes)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.Garrantys)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductsSubCategorys>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductsSubCategorys)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductsSubCategorys>()
                .HasOne(ps => ps.SubCategory)
                .WithMany(s => s.ProductsSubCategorys)
                .HasForeignKey(ps => ps.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}