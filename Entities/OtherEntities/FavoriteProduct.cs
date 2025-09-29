using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.AccountsEntities;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.OtherEntities
{
    public class FavoriteProduct : DbBaseProps
    {
        public required Guid ProductGuid { get; set; }
        public int UserAccountId { get; set; }
        public required UserAccount UserAccount { get; set; }
    }
}