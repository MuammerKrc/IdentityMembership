using IdentityMemberships.RoleProviderNames;
using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityMemberships.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Authorize(Roles = nameof(RoleProviderName.AdminRole))]
	public class HomeController : Controller
	{

		private readonly UserManager<AppUser> _userManager;


		public HomeController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public IActionResult Index()
		{


			return View();
		}

		public async Task<IActionResult> UserList()
		{
			var users = await _userManager.Users.ToListAsync();
			List<UserViewModel> viewModels = new();

			foreach (var i in users)
			{
				var response = await _userManager.GetRolesAsync(i) as List<string>;
				viewModels.Add(new UserViewModel()
				{
					Id = i.Id,
					City = i.City,
					Picture = i.Picture,
					Gender = i.Gender,
					BirthDay = i.Birthday,
					Email = i.Email,
					UserName = i.UserName,
					Roles = response
				});
			}

			return View(viewModels);
		}
	}
}
