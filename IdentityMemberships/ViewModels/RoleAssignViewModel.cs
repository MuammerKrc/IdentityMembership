using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStructureModel.ViewModels
{
    public class RoleAssignViewModel
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Exist { get; set; }
    }
}
