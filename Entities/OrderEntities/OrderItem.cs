using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;
using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.Entities.OrderEntities
{
    public class OrderItem : DbBaseProps
    {
        public required string ProductBrand { get; set; }
        public required string ProductTitle { get; set; }
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public int ProductGarrantyId { get; set; }
        public ProductGarranty? ProductGarranty { get; set; }
        public required int ProductCount { get; set; }
        public decimal Price { get; set; }
    }
}