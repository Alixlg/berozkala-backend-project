using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.Base;

namespace berozkala_backend.Entities.Product
{
    public class ProductAttribute : DbBase
    {
        public required string TitleName { get; set; }
        public required List<AttributeSubset> Subsets { get; set; }
    }
}