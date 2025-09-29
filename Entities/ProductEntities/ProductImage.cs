using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductImage : DbBaseProps
    {
        public required string ImagePath { get; set; }
        public string? ImageName { get; set; }
    }
}