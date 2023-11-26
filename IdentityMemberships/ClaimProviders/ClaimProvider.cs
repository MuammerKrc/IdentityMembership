using System.Security.Claims;
using IdentityMemberships.Extensions;
using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace IdentityMemberships.ClaimProviders
{
	//Added to DIServiceCollectionExtension then we created policy in IdentityServiceCollectionExtensions
	public class ClaimProvider : IClaimsTransformation
	{
		private readonly UserManager<AppUser> _userManager;

		public ClaimProvider(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		//Authentication olmuş her kişi için her sayfada buraya tekrar geliyor o yüzden pek tavsiye etmiyorum...
		public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			if (!principal.HasClaim(x => x.Type == "city"))
			{
				var user = await _userManager.FindByEmailAsync(principal.GetCurrentUserEmail());
				if (user != null && user.City != null)
				{

					Claim cityClaim = new Claim("city", user.City);
					(principal.Identity as ClaimsIdentity).AddClaim(cityClaim);

				}
			}
			return principal;
		}
	}
}
