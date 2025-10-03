using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductAttribute : DbBaseProps
    {
        public required string TitleName { get; set; }
        public required List<AttributeSubset> Subsets { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}