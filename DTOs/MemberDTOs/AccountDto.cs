using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.OtherEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.DTOs.MemberDTOs
{
    public class AccountDto
    {
        public required DateTime DateOfSingup { get; set; }
        public required AccountRole AccountRole { get; set; }
        public required string PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public required string UserName { get; set; }
        public List<AddressDto>? Addresses { get; set; }
        public string? Email { get; set; }
        public required AccountStatus Status { get; set; }
        public AccountGender Gender { get; set; }
        public string? NationalCode { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}