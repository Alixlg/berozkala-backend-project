using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class GarrantyAddToProductDto
    {
        public required Guid ProductId { get; set; }
        public required List<GarrantyDto> Garrantys { get; set; }
    }
}