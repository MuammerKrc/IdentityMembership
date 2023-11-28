using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityMemberships.PolicyProvider
{
	public class ExchangeExpireRequirements : IAuthorizationRequirement
	{
		//Policy authorization'da bir business işlem olduğundan dolayı Dı container için bir policy oluştururken buraya değer geçebiliyoruz.
		public int GetProp { get; set; }
	}

	public class ExchangeExpireRequirementsHandler : AuthorizationHandler<ExchangeExpireRequirements>
	{

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExchangeExpireRequirements requirement)
		{
			Console.WriteLine(requirement.GetProp);
			var existExchangeExpireClaim = context.User.HasClaim(claim => claim.Type == "ExchangeExpireDate");
			if (!existExchangeExpireClaim)
			{
				context.Fail();
				return Task.CompletedTask;
			}

			var exchangeExpireDate =
				context.User.Claims.FirstOrDefault(x => x.Type == "ExchangeExpireDate")!.Value;
			if (DateTime.Now > Convert.ToDateTime(exchangeExpireDate))
			{
				context.Fail();
				return Task.CompletedTask;
			}

			context.Succeed(requirement);
			return Task.CompletedTask;
		}
	}
}
