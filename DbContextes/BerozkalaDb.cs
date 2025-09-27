using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.AccountsEntities;
using berozkala_backend.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.DbContextes
{
    public class BerozkalaDb : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminAccount> Admins { get; set; }
        public DbSet<UserAccount> Users { get; set; }

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