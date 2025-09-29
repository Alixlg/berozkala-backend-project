using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.OtherEntities
{
    public class DiscountCode : DbBaseProps
    {
        public required bool IsActive { get; set; }
        public required string Code { get; set; }
        public required decimal DiscountAmount { get; set; }
        public required DateTime EndOfCredit { get; set; }
        public decimal MinProductPrice { get; set; }
        public int MaxUsageCount { get; set; }
    }
}