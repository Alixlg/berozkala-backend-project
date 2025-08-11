using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.Base;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductAttribute : DbBaseProps
    {
        public required string TitleName { get; set; }
        public required List<AttributeSubset> Subsets { get; set; }
    }
}