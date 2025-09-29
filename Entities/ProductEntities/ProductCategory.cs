using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductCategory : DbBaseProps
    {
        public required string CategoryName { get; set; }
        public List<ProductSubCategory>? SubCategorys { get; set; }
    }
}