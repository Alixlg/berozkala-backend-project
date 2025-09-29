using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductSubCategory : DbBaseProps
    {
        public required string SubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public required ProductCategory ProductCategory { get; set; }
        public List<Product>? Products { get; set; }
    }
}