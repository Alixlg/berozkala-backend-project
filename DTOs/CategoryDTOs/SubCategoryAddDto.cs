using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.DTOs.CategoryDTOs
{
    public class SubCategoryAddDto
    {
        public required string SubCategoryName { get; set; }
        public Guid? Id { get; set; }
        public SubCategory ToProductSubCategory(Category p)
        {
            return new SubCategory()
            {
                SubCategoryName = SubCategoryName,
                ProductCategory = p
            };
        }
    }
}