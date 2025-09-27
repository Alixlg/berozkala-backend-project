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
        public Guid Guid { get; set; } = Guid.NewGuid();
        public required DateTime DateOfSingup { get; set; }
        public required AccountRole AccountRole { get; set; }
        public required string PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
        public required string LastIp { get; set; }
        public string? Email { get; set; }
        public required AccountStatus Status { get; set; }
        public AccountGender Gender { get; set; } = AccountGender.Unknown;
        public string? NationalCode { get; set; }
        public int Age { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int OptCode { get; set; }
        public DateTime? OptLifeTime { get; set; }
    }
}