using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMemberships.Controllers
{
	public class ClaimController : Controller
	{
		public IActionResult Index()
		{
			var claims = User.Claims.ToList();


			return View(claims);
		}
		[Authorize("AnkaraPolicy")]
		public IActionResult AnkaraPolicyAuthorize()
		{
			return View();
		}
	}
}
