using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityMemberships.Areas.Admin.Controllers
{
	[Area("Admin")]
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
			var user = await _userManager.Users.ToListAsync();
			List<UserViewModel> viewModels = user.Select(i => new UserViewModel()
			{
				Id = i.Id,
				City = i.City,
				Picture = i.Picture,
				Gender = i.Gender,
				BirthDay = i.Birthday,
				Email = i.Email,
				UserName = i.UserName

			}).ToList();
			return View(viewModels);
		}
	}
}
