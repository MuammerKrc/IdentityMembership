using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityStructureModel.IdentityModels
{
    public class AppRole:IdentityRole<long>
    {


        public RoleViewModel GetRoleViewModel()
        {
            return new RoleViewModel()
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}
