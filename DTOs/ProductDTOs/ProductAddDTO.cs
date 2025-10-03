using berozkala_backend.DTOs.ProductSubDTOs;
using berozkala_backend.Entities.ProductEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.ProductDTOs
{
    public class ProductAddDto
    {
        public required bool IsAvailable { get; set; }
        public required string Brand { get; set; }
        public required string Title { get; set; }
        public List<Guid>? SubCategorys { get; set; }
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

        public List<ProductImage> ToProductImageListEntitie()
        {
            var imagesUrls = new List<ProductImage>();

            ImagesUrls?.ForEach(x =>
            {
                imagesUrls.Add(x.ToDatabaseEntitie());
            });

            return imagesUrls;
        }

        public List<ProductGarranty> ToProductGarrantyListEntitie()
        {
            var productGarrantys = new List<ProductGarranty>();

            Garrantys?.ForEach(x =>
            {
                productGarrantys.Add(x.ToDatabaseEntitie());
            });

            return productGarrantys;
        }

        public List<ProductAttribute> ToProductAttributeListEntitie()
        {
            var productAttributes = new List<ProductAttribute>();

            Attributes?.ForEach(x =>
            {
                productAttributes.Add(x.ToDatabaseEntitie());
            });

            return productAttributes;
        }
    }
}