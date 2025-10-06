using berozkala_backend.Entities.CommonEntities;
using berozkala_backend.Entities.OrderEntities;
using berozkala_backend.Entities.OtherEntities;
using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.Entities.AccountsEntities
{
    public class UserAccount : AccountBaseAttributes
    {
        public List<BasketProduct>? BasketsProducts { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<SubmitedDiscountCode>? SubmitedDiscountCodes { get; set; }
        public List<Order>? Orders { get; set; }
    }
}