using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berozkala_backend.Entities.CommonEntities;
using berozkala_backend.Enums;

namespace berozkala_backend.Entities.AccountsEntities
{
    public class AdminAccount : AccountBaseAttributes
    {
        public AdminRole AdminRole { get; set; }
        public List<AdminPermission>? AdminPermissions { get; set; }
    }
}