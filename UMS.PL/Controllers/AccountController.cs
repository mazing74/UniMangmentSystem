using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UMS.DAL.Models;
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

    }
}
