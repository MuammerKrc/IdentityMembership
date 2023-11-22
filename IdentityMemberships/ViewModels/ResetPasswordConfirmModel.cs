using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStructureModel.ViewModels
{
    public class ResetPasswordConfirmModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; } = false;

        [Required(ErrorMessage = "Şifre alanı gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "şifreniz en az 4 karakterli olmalıdır.")]
        public string PasswordNew { get; set; }
    }
}
