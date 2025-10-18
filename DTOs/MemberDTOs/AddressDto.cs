using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.MemberDTOs
{
    public class AddressDto
    {
        public Guid? Id { get; set; }
        public required string AddressBody { get; set; }
        public required string PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}