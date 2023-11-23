using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace IdentityMemberships.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult SignIn(string ReturnUrl)
		{
			TempData["ReturnUrl"] = ReturnUrl;
			return View(new LoginViewModel());
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(LoginViewModel model)
		{
			string failedExceptionDesc = "Kullanıcı adı veya şifre hatalı";
			bool lockoutUserWhenFailed = false;
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email.ToLower());
			if (user == null)
			{
				ModelState.AddModelError("", failedExceptionDesc);
				return View(model);
			}

			SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

			if (!signInResult.Succeeded)
			{
				if (signInResult.IsLockedOut)
				{
					ModelState.AddModelError("", "3 kere yanlış şifre girdiğiniz için 3 dakika kitlenmiştir.");
					return View(model);

				}
				ModelState.AddModelError("", failedExceptionDesc);
				return View(model);
			}

			if (TempData["ReturnUrl"] != null && !string.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
			{
				return Redirect(TempData["ReturnUrl"].ToString());
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
