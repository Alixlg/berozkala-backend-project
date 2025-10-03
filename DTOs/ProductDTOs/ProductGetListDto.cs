using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.ProductDTOs
{
    public class ProductGetListDto
    {
        public required int PageId { get; set; }
        public required int PageCount { get; set; }
        public bool? IsAvailable { get; set; }
        public ProductFillter? Fillter { get; set; }
        public string? SubCategoryName { get; set; }        
    }
}