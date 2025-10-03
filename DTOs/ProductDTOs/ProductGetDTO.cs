using berozkala_backend.DTOs.CategoryDTOs;
using berozkala_backend.DTOs.ProductSubDTOs;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.ProductDTOs
{
    public class ProductGetDto
    {
        public Guid Id { get; set; }
        public DateTime DateToAdd { get; set; }
        public required bool IsAvailable { get; set; }
        public required string Brand { get; set; }
        public required string Title { get; set; }
        public required List<SubCategoryGetDto> Category { get; set; }
        public required decimal Price { get; set; }
        public required int MaxCount { get; set; }
        public Score ScoreRank { get; set; }
        public decimal DiscountPercent { get; set; }
        public string? PreviewImageUrl { get; set; }
        public List<ImageDto>? ImagesUrls { get; set; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public List<GarrantyDto>? Garrantys { get; set; }
        public List<AttributeDto>? Attributes { get; set; }
    }
}