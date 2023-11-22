namespace IdentityMemberships.Models
{
	public class ErrorViewModel
	{
		public string? RequestId { get; set; }

		public bool ShowRequestIds => !string.IsNullOrEmpty(RequestId);
	}
}
