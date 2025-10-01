using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class Category : DbBaseProps
    {
        public required string CategoryName { get; set; }
        public List<SubCategory>? SubCategorys { get; set; }
    }
}