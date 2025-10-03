using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.DTOs.ProductSubDTOs;

namespace berozkala_backend.DTOs.ProductSubDtos
{
    public class ImageAddToProductDto
    {
        public required Guid ProductId { get; set; }
        public required List<ImageDto> Images { get; set; }
    }
}