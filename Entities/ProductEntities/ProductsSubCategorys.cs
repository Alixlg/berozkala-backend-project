using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class ProductSubCategory : DbBaseProps
    {
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
    }
}