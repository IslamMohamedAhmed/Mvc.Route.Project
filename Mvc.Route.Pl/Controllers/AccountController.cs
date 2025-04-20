using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Helper;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByNameAsync(registerViewModel.Username);
                    if (user is null)
                    {
                        user = await userManager.FindByEmailAsync(registerViewModel.Email);
                        if (user is null)
                        {
                            var res = new ApplicationUser()
                            {
                                UserName = registerViewModel.Username,
                                FirstName = registerViewModel.FirstName,
                                LastName = registerViewModel.LastName,
                                Email = registerViewModel.Email,
                                IsAgree = registerViewModel.IsAgree,
                            };
                            var result = await userManager.CreateAsync(res, registerViewModel.Password);
                            if (result.Succeeded)
                            {
                                return RedirectToAction(nameof(Login));
                            }
                            else
                            {
                                foreach (var i in result.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, i.Description);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Email is already used in this website!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username is already used in this website!");
                    }

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var flag = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This Email is not registered in the website!");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login!");
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ForgottenPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendResetUrl(ForgottenPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {

                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);
                    var EmailPattern = new EmailModel()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };
                    EmailSettings.SendEmail(EmailPattern);
                    return RedirectToAction(nameof(CheckYourInbox));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Operation, try again later!!");
                }
            }
            return View("ForgottenPassword", model);

        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid) {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = await userManager.FindByEmailAsync(email);
                if(user is not null)
                {
                    var result = await userManager.ResetPasswordAsync(user,token,model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                }
                
            }
            ModelState.AddModelError(string.Empty, "Invalid operation");
            return View(model);
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
