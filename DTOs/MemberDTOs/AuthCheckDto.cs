using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.MemberDTOs
{
    public class AuthCheckDto
    {
        public required AccountRole AccountRole { get; set; }
        public required bool IsSingIn { get; set; }
    }
}