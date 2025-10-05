using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class AttributeEditDto
    {
        public required Guid Id { get; set; }
        public required string TitleName { get; set; }
    }
}