using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityMemberships.CustomValidations
{
	public class CustomPasswordValidator : IPasswordValidator<AppUser>
	{
		public  Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
		{
			// add custom rule for password

			var errors = new List<IdentityError>();
			if (password.ToLower().Contains(user.UserName.ToLower()) )
			{
				errors.Add(new() {Code = "PasswordContainsUserName",Description = "Şifre alanı kullancı adı içeremez"});
			}


			if (!errors.Any())
			{
				
				return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
			}

			return Task.FromResult(IdentityResult.Success);
		}
	}
}
