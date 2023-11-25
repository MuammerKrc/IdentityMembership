using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityMemberships.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RoleController : Controller
	{

		private RoleManager<AppRole> _roleManager;

		public RoleController(RoleManager<AppRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public async Task<IActionResult> Index()
		{
			var response = await _roleManager.Roles.ToListAsync();
			return View(response);
		}
	}
}
