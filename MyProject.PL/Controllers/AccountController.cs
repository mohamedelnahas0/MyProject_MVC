using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.DAL.Models;
using MyProject.PL.Helpers;
using MyProject.PL.ViewModels.User;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MyProject.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> UserManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = UserManager;
			_signInManager = signInManager;
		}
		#region SignUp
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]

		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser()
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					IsAgree = model.IsAgree,
					Lname = model.Lname,
					Fname = model.Fname,
				};
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}
		#endregion


		#region Login
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var result = await _userManager.CheckPasswordAsync(user, model.Password);
					if (result)
					{
						var loginresult = await _signInManager.PasswordSignInAsync(user, model.Password, model.Rememberme, false);
						if (loginresult.Succeeded)
							return RedirectToAction("Index", "Home");

					}
					else

						ModelState.AddModelError(string.Empty, "Password is Incorrect");

				}
				else

					ModelState.AddModelError(string.Empty, "Email is not Exists");

			}
			return View(model);
		}

		#endregion


		#region SignOut
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion


		#region ForgetPassword
		public IActionResult ForgetPassword()
		{
			return View();
		}
        #endregion

      
        #region SendEmail
        [HttpPost]
		public async Task< IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
			var user= await _userManager.FindByEmailAsync(model.Email);
                if ( user is not null)
                {
					var token=_userManager.GeneratePasswordResetTokenAsync(user);
					var ResetPasswordlink = Url.Action("Resetpassword", "Account",new {email= user.Email ,Token = token}, Request.Scheme);
                    var email = new Email()
					{
						Subject = "Reset Password",
						To= model.Email,
						body ="ResetPasswordlink"

					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(checkyourInbox));
                }
				else
				{
					ModelState.AddModelError(string.Empty, "Email is not exist");
				}
            }	
				return RedirectToAction("ForgetPassword", model); 		
		}


		#endregion

         # region checkyourInbox
		public IActionResult checkyourInbox()
		{
			return View();
		}
		 #endregion
	}

}
