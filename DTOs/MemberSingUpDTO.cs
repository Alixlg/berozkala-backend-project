using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs
{
    public class MemberSingUpDTO
    {
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
    }
}