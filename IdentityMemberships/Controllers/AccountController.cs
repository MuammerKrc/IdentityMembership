using Microsoft.AspNetCore.Mvc;

namespace IdentityMemberships.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult SignIn()
		{
			return View();
		}
	}
}
