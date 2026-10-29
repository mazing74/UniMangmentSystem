using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UMS.DAL.Models;
using UMS.PL.Helpers;
using UMS.PL.ViewModels;

namespace UMS.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerVM.Email.Split("@")[0],
                    FullName = registerVM.FullName,
                    Email = registerVM.Email,
                    IsAgree = registerVM.IsAgree
                };

                var result = await _userManger.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(" ", error.Description);



            }

            return View(registerVM);


        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(loginVM.Email);

                if (user is not null)
                {

                    bool found = await _userManger.CheckPasswordAsync(user, loginVM.Password);

                    if (found)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(" ", "Invalid username or password");
                    }
                }

                else
                {
                    ModelState.AddModelError(" ", "Invalid username or password");
                }


            }

            return View(loginVM);
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

        public IActionResult ForgotPassword()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var token = await _userManger.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = user.Email, Token = token }, "https", "localhost:7178");
                    // Send Email
                    var email = new Email()
                    {
                        Subject = "Reset Password For Uni Mangment System",
                        To = model.Email,
                        Body = ResetPasswordLink,
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckInbox));
                }
                ModelState.AddModelError(" ", " Email Not Exist");
            }

            return View("ForgotPassword", model);
        }

        public IActionResult CheckInbox()
        {
            return View();

        }
        public IActionResult ResetPassword(string Email, string Token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM passwordVM)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["Email"] as string;
                string token = TempData["Token"] as string;

                var user = await _userManger.FindByEmailAsync(email);
                var result = await _userManger.ResetPasswordAsync(user, token, passwordVM.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(" ", error.Description);
            }

            return View(passwordVM);
        }

        #endregion
    }
}
