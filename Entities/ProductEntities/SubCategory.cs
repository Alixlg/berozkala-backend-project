using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class SubCategory : DbBaseProps
    {
        public required string SubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public required Category ProductCategory { get; set; }
        public List<ProductsSubCategorys>? ProductsSubCategorys { get; set; }
    }
}