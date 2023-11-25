using Microsoft.Identity.Client;

namespace IdentityMemberships.ServiceCollectionExtensions
{
	public static class CookieServiceCollectionExtensions
	{
		public static void CookieConfigurationExtensions(this IServiceCollection Services)
		{

			Services.ConfigureApplicationCookie(opt =>
			{
				var cookieBuilder = new CookieBuilder();
				cookieBuilder.Name = "IdentityCookies";

				opt.LoginPath = new PathString("/Account/SignIn");
				opt.Cookie = cookieBuilder;
				opt.ExpireTimeSpan = TimeSpan.FromDays(60);
				opt.SlidingExpiration = true;

			});
		}
	}
}
