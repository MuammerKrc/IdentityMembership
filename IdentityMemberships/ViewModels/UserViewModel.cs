﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityStructureModel.IdentityModels;

namespace IdentityStructureModel.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı ismi gerekldir.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [Display(Name = "Email Adresiniz")]
        [EmailAddress(ErrorMessage = "Email adresiniz doğru formatta değil")]
        public string Email { get; set; }

        [Display(Name = "Doğum tarihi")]
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        public string Picture { get; set; }

        [Display(Name = "Şehir")]
        public string City { get; set; }

        public Gender Gender { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public void UpdateUser(AppUser user)
        {
            user.UserName = UserName;
            user.Email = Email;
            user.City = City;
            if(BirthDay.HasValue)
                user.Birthday = BirthDay.Value;
            user.Picture = Picture;
            user.City = City;
            user.Gender = Gender;
        }
    }
}
