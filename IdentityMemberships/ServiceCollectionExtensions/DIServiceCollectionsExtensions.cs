using IdentityMemberships.ClaimProviders;
using IdentityMemberships.Services;
using Microsoft.AspNetCore.Authentication;

namespace IdentityMemberships.ServiceCollectionExtensions
{
	public static class DIServiceCollectionsExtensions
	{
		public static void DIConfigurationExtensions(this IServiceCollection Services)
		{
			Services.AddScoped<IEmailService, EmailService>();
			Services.AddScoped<IClaimsTransformation, ClaimProvider>();
		}
	}
}
