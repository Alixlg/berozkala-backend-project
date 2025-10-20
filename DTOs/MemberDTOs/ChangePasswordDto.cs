using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.MemberDTOs
{
    public class ChangePasswordDto
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}