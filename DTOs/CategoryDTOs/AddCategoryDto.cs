using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.DTOs.CategoryDTOs
{
    public class AddCategoryDto
    {
        public required string CategoryName { get; set; }
        public List<ProductSubCategory>? SubCategorys { get; set; }
    }

    // public record ProductSubCategoryDto(Guid? Id, string SubCategoryName);
}