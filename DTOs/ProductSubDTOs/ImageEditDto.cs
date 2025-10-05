using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class ImageEditDto
    {
        public required Guid Id { get; set; }
        public required string ImagePath { get; set; }
        public string? ImageName { get; set; }
    }
}