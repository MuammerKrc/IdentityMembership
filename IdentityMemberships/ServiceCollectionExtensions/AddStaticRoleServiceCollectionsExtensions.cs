using System.Reflection;
using IdentityMemberships.RoleProviderNames;
using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace IdentityMemberships.ServiceCollectionExtensions
{
	public static class AddStaticRoleServiceCollectionsExtensions
	{
		public static void AddStaticRolesConfigurationExtensions(this IServiceCollection collection)
		{
			var serviceProvider = collection.BuildServiceProvider();
			using (var roleManager = serviceProvider.GetService<RoleManager<AppRole>>())
			{
				var rolesFields = typeof(RoleProviderName).GetFields();

				foreach (var rolesProperty in rolesFields)
				{
					var roleName = typeof(RoleProviderName).GetField(rolesProperty.Name)?
						.GetValue(BindingFlags.GetField) as string;

					bool existRole = roleManager.RoleExistsAsync(roleName).Result;

					if (!existRole)
					{
						var IdentityResult = roleManager.CreateAsync(new AppRole()
						{
							Name = roleName,
							ConcurrencyStamp = Guid.NewGuid().ToString()
						}).Result;
					}
				}
			}

		}
	}
}
