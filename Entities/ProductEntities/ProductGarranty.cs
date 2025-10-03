using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductGarranty : DbBaseProps
    {
        public required string Name { get; set; }
        public long GarrantyCode { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}