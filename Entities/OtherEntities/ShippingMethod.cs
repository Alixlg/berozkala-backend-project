using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.OtherEntities
{
    public class ShippingMethod : DbBaseProps
    {
        public required string MethodName { get; set; }
        public required string MethodDescription { get; set; }
        public decimal ShipmentCost { get; set; }
    }
}