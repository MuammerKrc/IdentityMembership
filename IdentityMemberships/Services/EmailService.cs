using System.Net;
using System.Net.Mail;
using IdentityMemberships.ConfigurationModels;
using Microsoft.Extensions.Options;

namespace IdentityMemberships.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailConfigurationModel _emailConfiguration;

		public EmailService(IOptions<EmailConfigurationModel> options)
		{
			_emailConfiguration = options.Value;
		}

		public async Task SendForgetEmail(string toEmail, string link)
		{

			var smtpClient = new SmtpClient();
			smtpClient.Host = _emailConfiguration.Host;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Port = 587;
			smtpClient.Credentials = new NetworkCredential(_emailConfiguration.Email, _emailConfiguration.Password);
			smtpClient.EnableSsl = true;
			var mailMessage = new MailMessage()
			{
				From = new MailAddress(_emailConfiguration.Email),
				To = { toEmail },
				Subject = "Şifre sıfırlama linki",
				Body = $@"<h2>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h2>
					   </br>
					   <a> {link}</a>",
				IsBodyHtml = true,
			};
			await smtpClient.SendMailAsync(mailMessage);

		}
	}
}
