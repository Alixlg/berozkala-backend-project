using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.Base;

namespace berozkala_backend.Entities.ProductEntities
{
    public class BasketProduct : DbBaseProps
    {
        public required Product Product { get; set; }
        public int Count { get; set; }
    }
}