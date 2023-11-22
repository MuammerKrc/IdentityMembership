using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityStructureModel.IdentityModels
{
    public enum Gender
    {
        Man=0,
        Women=1,
    }
    public class AppUser:IdentityUser<long>
    {
        public string City { get; set; }
        public string Picture { get; set; }
        public DateTime? Birthday { get; set; }
        public  Gender Gender { get; set; }
        public UserViewModel CreateUserViewModel()
        {
            return new UserViewModel()
            {
                Email = this.Email,
                UserName = this.UserName,
                City = this.City,
                Picture = this.Picture,
                BirthDay = Birthday??DateTime.Now,
                Gender=Gender,
            };
        }
    }
}
