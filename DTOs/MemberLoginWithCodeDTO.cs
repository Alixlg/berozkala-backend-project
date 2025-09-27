using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs
{
    public class MemberLoginWithCodeDTO
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}