using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStructureModel.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [EmailAddress]
        public string Email { get; set; }

        public bool Success { get; set; } = false;
    }
}
