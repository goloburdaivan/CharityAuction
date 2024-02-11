using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RzhadBids.Auth;
using RzhadBids.Services;
using RzhadBids.ViewModels;

namespace RzhadBids.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(DatabaseContext context) : base(context)
        {
        }

        [HttpGet("/login")]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Неправильні дані для входу");
                return View(model);
            }
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/register")]
        public IActionResult Register(string returnUrl = null)
        {
            return View();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser {
                UserName = model.Email, Email = model.Email,
                Name = model.Name,
                Surname = model.Surname
            };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
				switch (error.Code)
				{
					case "DuplicateUserName":
						ModelState.AddModelError("Email", "Акаунт з такою адресою вже існує");
						break;
					case "PasswordRequiresDigit":
						ModelState.AddModelError("Password", "Пароль має містити хоча б одну цифру");
						break;
					case "PasswordRequiresLower":
						ModelState.AddModelError("Password", "Пароль має містити хоча б одну малу літеру");
						break;
					case "PasswordRequiresUpper":
						ModelState.AddModelError("Password", "Пароль має містити хоча б одну велику літеру");
						break;
					case "PasswordRequiresNonAlphanumeric":
						ModelState.AddModelError("Password", "Пароль має містити хоча б один не буквено-цифровий символ");
						break;
					default:
						ModelState.AddModelError(string.Empty, error.Description);
						break;
				}
			}
            return View(model);
        }

        private IActionResult RedirectIfLoggedIn()
        {
            return Redirect("/profile");
        }

        [HttpGet("/404")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}