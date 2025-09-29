using berozkala_backend.Entities.AccountsEntities;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.ProductEntities
{
    public class BasketProduct : DbBaseProps
    {
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public int UserId { get; set; }
        public required UserAccount User { get; set; }
        public int ProductCount { get; set; }
        public ProductGarranty? SelectedGarranty { get; set; }
    }
}