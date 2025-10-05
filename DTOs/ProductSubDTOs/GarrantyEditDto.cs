using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class GarrantyEditDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public long GarrantyCode { get; set; }
    }
}