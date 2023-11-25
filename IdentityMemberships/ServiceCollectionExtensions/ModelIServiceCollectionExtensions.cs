using IdentityMemberships.ConfigurationModels;

namespace IdentityMemberships.ServiceCollectionExtensions
{
	public static class ModelIServiceCollectionExtensions
	{
		public static void ModelConfigureExtensions(this IServiceCollection Services,IConfiguration Configuration)
		{
			Services.Configure<EmailConfigurationModel>(Configuration.GetSection("EmailSettings"));
		}
	}
}
