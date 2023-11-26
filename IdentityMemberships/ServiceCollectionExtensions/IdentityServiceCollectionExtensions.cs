using IdentityMemberships.CustomValidations;
using IdentityMemberships.Localizations;
using IdentityStructureModel.IdentityDbContexts;
using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityMemberships.ServiceCollectionExtensions
{
	public static class IdentityServiceCollectionExtensions
	{
		public static void IdentityConfigurationExtensions(this IServiceCollection Services)
		{


			//Identity için olu?turulan token süresi reset password change email
			Services.Configure<DataProtectionTokenProviderOptions>(opt =>
			{
				opt.TokenLifespan = TimeSpan.FromMinutes(10);
			});
			//Services.Configure<SecurityStampValidatorOptions>(options =>
			//{
			//	options.ValidationInterval = TimeSpan.FromSeconds(10);
			//});

			Services.AddIdentity<AppUser, AppRole>(delegate (IdentityOptions options)
				{
					//password
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequiredLength = 6;
					options.Password.RequiredUniqueChars = 4;
					//user
					options.User.RequireUniqueEmail = true;
					//lockout-enabled
					options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
					options.Lockout.MaxFailedAccessAttempts = 3;
					options.Lockout.AllowedForNewUsers = true;
				})
				.AddPasswordValidator<CustomPasswordValidator>()
				.AddUserValidator<CustomUserValidator>()
				.AddErrorDescriber<LocalizationIdentityErrorDescription>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AppIdentityDbContext>();

			//Add Ankara Policy Here
			Services.AddAuthorization(options =>
			{
				options.AddPolicy("AnkaraPolicy", policy =>
				{
					policy.RequireClaim("city", "Ankara");
				});
			});
		}
	}
}
