using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs
{
    public class MemberSubmitCodeDTO
    {
        public required string PhoneNumber { get; set; }
        public required int OptCode { get; set; }
    }
}