using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.BasketDTOs
{
    public class ProductToBasketDto
    {
        public required Guid ProductId { get; set; }
        public Guid? SelectedGarranty { get; set; }
    }
}