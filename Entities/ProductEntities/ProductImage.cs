using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductImage : DbBaseProps
    {
        public required string ImagePath { get; set; }
        public string? ImageName { get; set; }
    }
}