using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMemberships.Controllers
{
	[Authorize]
	public class MemberController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IActionResult> Index()
		{
			var currentUser = await _userManager.FindByNameAsync(User?.Identity?.Name ?? "");
			return View(currentUser);
		}
	}
}
