using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.DTOs.CategoryDTOs
{
    public class GetCategoryDto
    {
        public Guid Id { get; set; }
        public required string CategoryName { get; set; }
        public List<ProductSubCategory>? SubCategorys { get; set; }
    }
}