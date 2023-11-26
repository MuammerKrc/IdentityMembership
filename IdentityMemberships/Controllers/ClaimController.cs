using Microsoft.AspNetCore.Mvc;

namespace IdentityMemberships.Controllers
{
	public class ClaimController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
