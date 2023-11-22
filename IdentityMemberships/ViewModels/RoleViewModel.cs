using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityStructureModel.IdentityModels;

namespace IdentityStructureModel.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "Role ismi")]
        [Required(ErrorMessage = "Role ismi gereklidir")]
        public string Name { get; set; }

        public long Id { get; set; }

        public AppRole CreateRole()
        {
            return new AppRole()
            {
                Name = this.Name
            };
        }


        public void UpdateRole(AppRole role)
        {
            role.Name = this.Name;
        }

        public void ResetRole()
        {
            this.Name = "";
        }
    }
}
