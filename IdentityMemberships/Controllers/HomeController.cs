using IdentityMemberships.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityMemberships.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<AppUser> _userManager;

		public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
		{
			_logger = logger;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}


		public async Task<IActionResult> SignUp(string ReturnUrl = "")
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(UserSignUpViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = model.CreateUser();
			user.City = "asd";
			user.Picture = "akkaka";
			user.Gender = Gender.Man;
			IdentityResult result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				foreach (var identityError in result.Errors)
					ModelState.AddModelError(identityError.Code, identityError.Description);

				return View(model);
			}

			return RedirectToAction("SignIn","Account");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


	}
}
