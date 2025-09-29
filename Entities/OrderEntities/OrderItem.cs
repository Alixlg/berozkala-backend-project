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
        public required Guid ProductGuid { get; set; }
        public ProductGarranty? ProductGarranty { get; set; }
        public required int ProductCount { get; set; }
        public decimal UnitPrice { get; set; } // قیمت یک واحد محصول
        public decimal TotalPrice { get; set; } // جمع کل
    }
}