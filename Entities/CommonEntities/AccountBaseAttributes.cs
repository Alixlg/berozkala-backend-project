using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.Base;
using berozkala_backend.Enums;

namespace berozkala_backend.Entities.CommonEntities
{
    public abstract class AccountBaseAttributes : DbBaseProps
    {
        public Guid Guid { get; set; } = System.Guid.NewGuid();
        public required DateTime DateOfSingup { get; set; }
        public required bool IsVisible { get; set; }
        public required AccountRole AccountRole { get; set; }
        public required ulong PhoneNumber { get; set; }
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
        public required string LastIp { get; set; }
        public string? Email { get; set; }
        public required AccountStatus Status { get; set; }
        public AccountGender Gender { get; set; } = AccountGender.Unknown;
        public ulong NationalCode { get; set; }
        public int Age { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}