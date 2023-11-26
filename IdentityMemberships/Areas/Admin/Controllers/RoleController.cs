using IdentityMemberships.RoleProviderNames;
using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IdentityMemberships.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = nameof(RoleProviderName.AdminRole))]
	public class RoleController : Controller
	{

		private readonly RoleManager<AppRole> _roleManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public async Task<IActionResult> Index()
		{
			var response = await _roleManager.Roles.ToListAsync();
			return View(response);
		}

		public async Task<IActionResult> RoleAssign(string userId)
		{
			RoleAssignViewModel roleAssign = new RoleAssignViewModel();

			AppUser user = await _userManager.FindByIdAsync(userId);
			roleAssign.UserId = userId;

			roleAssign.UserName = user.UserName;
			List<AppRole> roles = _roleManager.Roles.ToList();
			List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;

			foreach (var item in roles)
			{
				RoleExistViewModel model = new();
				model.RoleId = item.Id;
				model.RoleName = item.Name;
				model.Exist = userRoles.Contains(item.Name);
				roleAssign.RoleExistViewModels.Add(model);
			}

			return View(roleAssign);
		}

		[HttpPost]
		public async Task<IActionResult> RoleAssign(RoleAssignViewModel roleAssign)
		{

			AppUser user = await _userManager.FindByIdAsync(roleAssign.UserId);
			foreach (var item in roleAssign.RoleExistViewModels)
			{
				if (item.Exist)
					await _userManager.AddToRoleAsync(user, item.RoleName);
				else
					await _userManager.RemoveFromRoleAsync(user, item.RoleName);
			}

			var result =await _userManager.UpdateSecurityStampAsync(user);
			if (user.UserName == roleAssign.UserName)
			{
				await _signInManager.SignOutAsync();
				await _signInManager.SignInAsync(user, true);

			}
			return RedirectToAction("Index", "Home");
		}
	}
}
