using berozkala_backend.Entities.CommonEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.Entities.ProductEntities
{
    public class Product : DbBaseProps
    {
        public DateTime DateToAdd { get; set; } = DateTime.Now;
        public required bool IsAvailable { get; set; }
        public required string Brand { get; set; }
        public required string Title { get; set; }
        public List<ProductsSubCategorys>? ProductsSubCategorys { get; set; }
        public required decimal Price { get; set; }
        public required int MaxCount { get; set; }
        public Score ScoreRank { get; set; }
        public decimal DiscountPercent { get; set; }
        public string? PreviewImageUrl { get; set; }
        public List<ProductImage>? ImagesUrls { get; set; }
        public string? Description { get; set; }
        public string? Review { get; set; }
        public List<ProductGarranty>? Garrantys { get; set; }
        public List<ProductAttribute>? Attributes { get; set; }
    }
}