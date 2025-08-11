

using berozkala_backend.Entities.CommonEntities;
using berozkala_backend.Entities.ProductEntities;

namespace berozkala_backend.Entities.AccountsEntities
{
    public class UserAccount : AccountBaseAttributes
    {
        public List<BasketProduct>? BasketProducts { get; set; }
    }
}