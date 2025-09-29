using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductSubCategory : DbBaseProps
    {
        public required string SubCategoryName { get; set; }
    }
}