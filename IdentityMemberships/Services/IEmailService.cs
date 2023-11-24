namespace IdentityMemberships.Services
{
	public interface IEmailService
	{
		Task SendForgetEmail(string toEmail, string link);
	}
}
