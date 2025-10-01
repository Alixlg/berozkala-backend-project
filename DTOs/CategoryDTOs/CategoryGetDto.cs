namespace berozkala_backend.DTOs.CategoryDTOs
{
    public class CategoryGetDto
    {
        public Guid Id { get; set; }
        public required string CategoryName { get; set; }
        public List<SubCategoryAddDto>? SubCategorys { get; set; }
    }
}