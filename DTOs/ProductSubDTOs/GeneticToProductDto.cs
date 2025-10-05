using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class GeneticToProductDto<T>
    {
        public required Guid EntityId { get; set; }
        public required List<T> Items { get; set; }
    }
}