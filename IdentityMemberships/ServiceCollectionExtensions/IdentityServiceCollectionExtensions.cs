using IdentityMemberships.CustomValidations;
using IdentityMemberships.Localizations;
using IdentityMemberships.PolicyProvider;
using IdentityStructureModel.IdentityDbContexts;
using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Authorization;
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
				//claims authroization için policy oluşturuldu
				options.AddPolicy("AnkaraPolicy", policy =>
				{
					policy.RequireClaim("city", "Ankara");
				});

				//policy authroization için policy oluşturuldu
				options.AddPolicy("ExchangePolicy", policy =>
				{
					policy.AddRequirements(new ExchangeExpireRequirements()
					{
						//sınıf içerisinde property örneklemesini yaptık.
						GetProp = 12
					});
				});

				//claims baslı authorizationn için oluşturuldu ancak bu db üzerinde tutulmaz
				options.AddPolicy("BirthdayExchange", policy =>
				{
					policy.AddRequirements(new BirthdayRequirements()
					{
						//sınıf içerisinde property örneklemesini yaptık.
						Threshold = 30
					});
				});
			});
			Services.AddScoped<IAuthorizationHandler, ExchangeExpireRequirementsHandler>();
			Services.AddScoped<IAuthorizationHandler, BirthdayRequirementsHandler>();
		}
	}
}
