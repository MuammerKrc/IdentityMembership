using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStructureModel.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Eski şifreniz")]
        [Required(ErrorMessage = "Eski şifreniz gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Şifreniz en az 5 karakterli olmak zorundadır.")]
        public string PasswordOld { get; set; }

        [Display(Name = "Yeni şifreniz")]
        [Required(ErrorMessage = "Yeni şifreniz gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Şifreniz en az 5 karakterli olmak zorundadır.")]
        public string PasswordNew { get; set; }

        [Display(Name = "Onay yeni şifreniz")]
        [Required(ErrorMessage = "Onay yeni şifre gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Şifreniz en az 5 karakterli olmak zorundadır.")]
        [Compare("PasswordNew", ErrorMessage = "Yeni şifreniz  ve onay şifreniz birbirinden farklıdır.")]
        public string PasswordConfirm { get; set; }


        public void ResetModel()
        {
            this.PasswordNew = "";
            this.PasswordOld = "";
            this.PasswordConfirm = "";
        }
    }
}
