using System.Security.Claims;
using IdentityMemberships.Services;
using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace IdentityMemberships.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IEmailService _emailService;
		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailService emailService)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_emailService = emailService;
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



			//********************
			//ClaimOlmadan Giriş Yapıldığında Aşağıdaki kullanılabilir.
			//SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

			//********************
			//Claim ile giriş yapmak istediğimde aşağıdaki gibi giriş yapacağım.

			var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
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
			await _signInManager.SignInWithClaimsAsync(user, true, new List<Claim>());




			if (TempData["ReturnUrl"] != null && !string.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
			{
				return Redirect(TempData["ReturnUrl"].ToString());
			}

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> SignOut()
		{
			if (HttpContext.User.Identity?.IsAuthenticated ?? false)
			{
				await _signInManager.SignOutAsync();
			}

			return RedirectToAction("Index", "Home");
		}
		public async Task<IActionResult> ResetPassword(string userId, string token)
		{
			if (userId == null || token == null)
			{
				return RedirectToAction("Index", "Home");
			}

			return View(new ResetPasswordConfirmModel() { UserId = userId, Token = token });
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordConfirmModel model)
		{

			if (!ModelState.IsValid) View(model);

			var user = await _userManager.FindByIdAsync(model.UserId);
			if (user == null)
			{
				ModelState.AddModelError("", "Bu kullanıcı bulunamadı.");
				return View(model);
			}

			var result = await _userManager.ResetPasswordAsync(user, model.Token, model.PasswordNew);
			if (!result.Succeeded)
			{
				result.Errors.ToList().ForEach(i =>
				{
					ModelState.AddModelError("", i.Description);
				});
				return View(model);
			}

			await _userManager.UpdateSecurityStampAsync(user);
			model.Success = true;
			ViewBag.status = "success";
			return View(model);

		}
		public async Task<IActionResult> ForgetPassword()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
		{
			var user = await _userManager.FindByEmailAsync(viewModel.Email);

			if (user == null)
			{

				ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı");
				return View(viewModel);

			}
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var passwordRestLink = Url.Action("ResetPassword", "Account", new
			{
				userId = user.Id,
				token = token
			}, HttpContext.Request.Scheme, "localhost:44344");



			TempData["success"] = "Şifre yenileme linki, eposta adresinize gönderilmiştir";
			await _emailService.SendForgetEmail(user.Email, passwordRestLink);


			return RedirectToAction("ForgetPassword");
		}

	}
}
