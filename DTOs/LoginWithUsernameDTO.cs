using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs
{
    public class LoginWithUsernameDTO
    {
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
    }
}