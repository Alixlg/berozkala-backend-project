using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace berozkala_backend.DbContextes
{
    public class BerozkalaDb : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGarranty> ProductGarrantys { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AttributeSubset> AttributeSubsets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=DbFiles\BerozkalaDb.sqlite");
        }
    }
}