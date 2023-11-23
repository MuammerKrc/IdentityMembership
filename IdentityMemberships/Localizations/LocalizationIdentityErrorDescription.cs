using Microsoft.AspNetCore.Identity;

namespace IdentityMemberships.Localizations
{
	public class LocalizationIdentityErrorDescription : IdentityErrorDescriber
	{
		// can override errors message
		public override IdentityError DuplicateUserName(string userName)
		{
			return new IdentityError() { Code = "DuplicateUserName", Description = "Bu kullanıcı adını daha önceden alınmıştır." };
		}
	}
}
