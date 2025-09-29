using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.AccountsEntities;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.OtherEntities
{
    public class SubmitedDiscountCode : DbBaseProps
    {
        public int UserId { get; set; }
        public required UserAccount User { get; set; }
        public int DiscountCodeId { get; set; }
        public required DiscountCode DiscountCode { get; set; }
    }
}