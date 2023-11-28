using System.Security.Claims;
using IdentityStructureModel.IdentityModels;
using IdentityStructureModel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			return View(new UserViewModel()
			{
				UserName = currentUser.UserName,
				Email = currentUser.Email,
				City = currentUser.City,
				BirthDay = currentUser.Birthday ?? DateTime.Now,
				Gender = currentUser.Gender,
				Id = currentUser.Id,
				Picture = currentUser.Picture
			});
		}

		public async Task<IActionResult> ChangePassword()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
		{
			if (!ModelState.IsValid) return View(viewModel);


			var currentUser = await _userManager.FindByNameAsync(User?.Identity?.Name ?? "");

			var oldPasswordCheck = await _userManager.CheckPasswordAsync(currentUser, viewModel.PasswordOld);
			if (!oldPasswordCheck)
			{
				ModelState.AddModelError("", "Lütfen eski şifrenizi doğru giriniz");
				return View(viewModel);
			}

			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
			var response = await _userManager.ChangePasswordAsync(currentUser, viewModel.PasswordOld, viewModel.PasswordNew);
			if (response.Succeeded)
			{
				await _userManager.UpdateSecurityStampAsync(currentUser);
				await _signInManager.SignOutAsync();
				await _signInManager.SignInAsync(currentUser, true);
				ViewBag.success = "true";
				return View();
			}
			foreach (var responseError in response.Errors)
			{
				ModelState.AddModelError("", responseError.Description);
			}

			return View();
		}

		public async Task<IActionResult> UserEdit()
		{
			var user = await _userManager.FindByNameAsync(User?.Identity?.Name ?? "");
			var userViewModel = user.CreateUserViewModel();
			ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)), user.Gender);
			return View(userViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> UserEdit(UserViewModel model, IFormFile userPicture)
		{
			if (ModelState.IsValid) View(model);
			ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)), model.Gender);

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			model.UpdateUser(user);
			var result = await _userManager.UpdateAsync(user);

			if (userPicture != null && userPicture.Length > 0)
			{
				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPicture", fileName);
				var directoryFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPicture");

				if (!Directory.Exists(directoryFilePath))
					Directory.CreateDirectory(directoryFilePath);


				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await userPicture.CopyToAsync(stream);
					user.Picture = "/UserPicture/" + fileName;
				}
			}
			if (!result.Succeeded)
			{
				result.Errors.ToList().ForEach(i =>
				{
					ModelState.AddModelError("", i.Description);
				});
				return View(model);
			}


			await _userManager.UpdateSecurityStampAsync(user);
			await _signInManager.SignOutAsync();
			if (user.Birthday.HasValue)
			{
				await _signInManager.SignInWithClaimsAsync(user, true, additionalClaims: new List<Claim>() { new Claim("Birthday", user.Birthday.HasValue ? user.Birthday.Value.ToString() : DateTime.Now.ToString()) });
			}
			else
			{
				await _signInManager.SignInAsync(user, true);

			}
			ViewBag.success = "true";
			return View(model);
		}

	}
}
