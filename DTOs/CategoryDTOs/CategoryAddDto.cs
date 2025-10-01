
namespace berozkala_backend.DTOs.CategoryDTOs
{
    public class CategoryAddDto
    {
        public required string CategoryName { get; set; }
        public List<SubCategoryAddDto>? SubCategorys { get; set; }
    }
}