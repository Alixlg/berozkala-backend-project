using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class GarrantyDeleteToProductDto
    {
        public required Guid ProductId { get; set; }
        public required List<Guid> GarrantyIds { get; set; }
    }
}