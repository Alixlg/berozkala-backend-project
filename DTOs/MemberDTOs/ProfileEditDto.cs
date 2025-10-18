using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.MemberDTOs
{
    public class ProfileEditDto
    {
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public AccountGender Gender { get; set; }
        public string? NationalCode { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}