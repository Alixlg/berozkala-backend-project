using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.DTOs.ProductSubDTOs
{
    public class ImageDto
    {
        public Guid? Id { get; set; }
        public required string ImagePath { get; set; }
        public string? ImageName { get; set; }
        public ProductImage ToDatabaseEntitie()
        {
            return new ProductImage()
            {
                ImagePath = ImagePath,
                ImageName = ImageName
            };
        }
    }
}