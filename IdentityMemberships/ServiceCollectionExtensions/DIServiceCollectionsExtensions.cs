using IdentityMemberships.Services;

namespace IdentityMemberships.ServiceCollectionExtensions
{
	public static class DIServiceCollectionsExtensions
	{
		public static void DIConfigurationExtensions(this IServiceCollection Services)
		{
			Services.AddScoped<IEmailService, EmailService>();
		}
	}
}
