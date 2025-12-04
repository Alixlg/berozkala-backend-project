using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.Entities.OtherEntities
{
    public class PaymentInformaation : DbBaseProps
    {
        public required PaymentMethod PaymentMethod { get; set; }
        public required PaymentStatus PaymentStatus { get; set; }
        public string? PaymentTrackingCode { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal Amount { get; set; }
        public required bool IsApproved { get; set; }
    }
}