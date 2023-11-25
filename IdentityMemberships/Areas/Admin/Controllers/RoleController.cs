using Microsoft.AspNetCore.Mvc;

namespace IdentityMemberships.Areas.Admin.Controllers
{
	public class RoleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
