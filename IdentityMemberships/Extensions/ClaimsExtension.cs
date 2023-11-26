using System.Security.Authentication;
using System.Security.Claims;

namespace IdentityMemberships.Extensions
{
	public static class ClaimsExtension
	{
		public static string GetCurrentUserEmail(this ClaimsPrincipal principal)
		{
			return principal.Claims.Where(i => i.Type == ClaimTypes.Email)?.FirstOrDefault()?.Value.ToString() ?? "";
		}
	}
}
