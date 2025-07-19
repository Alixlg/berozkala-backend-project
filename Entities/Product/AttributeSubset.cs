using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.Base;

namespace berozkala_backend.Entities.Product
{
    public class AttributeSubset : DbBase
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}