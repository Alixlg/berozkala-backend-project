using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;

namespace berozkala_backend.Entities.OtherEntities
{
    public class Address : DbBaseProps
    {
        public required string AddressBody { get; set; }
        public required string PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}