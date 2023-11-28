using Microsoft.AspNetCore.Authorization;

namespace IdentityMemberships.PolicyProvider
{
	public class BirthdayRequirements : IAuthorizationRequirement
	{
		public int Threshold { get; set; }
	}

	public class BirthdayRequirementsHandler : AuthorizationHandler<BirthdayRequirements>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BirthdayRequirements requirement)
		{
			var existBirthday = context.User.HasClaim(claim => claim.Type == "Birthday");
			if (!existBirthday)
			{
				context.Fail();
				return Task.CompletedTask;
			}

			var birthday =
				context.User.Claims.FirstOrDefault(x => x.Type == "Birthday")!.Value;
			if (DateTime.Now.Year - Convert.ToDateTime(birthday).Year < requirement.Threshold)
			{
				context.Fail();
				return Task.CompletedTask;
			}

			context.Succeed(requirement);
			return Task.CompletedTask;
		}
	}

}
