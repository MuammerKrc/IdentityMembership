using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityMemberships.CustomValidations
{
	public class CustomUserValidator : IUserValidator<AppUser>
	{
		public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
		{
			// add custom rule for user
			var errors = new List<IdentityError>();
			bool startWithNumeric = int.TryParse(user.UserName[0].ToString(), out _);
			if (startWithNumeric)
			{
				errors.Add(new IdentityError() { Code = "startedNumeric", Description = "Kullanıcı adı rakam ile başlayamaz." });
			}

			if (errors.Any())
			{
				return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
			}

			return Task.FromResult(IdentityResult.Success);
		}
	}
}
