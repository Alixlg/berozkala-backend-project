using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.ProductSubDtos
{
    public class ImageDeleteToProductDto
    {
        public required Guid ProductId { get; set; }
        public required List<Guid> ImageIds { get; set; }
    }
}