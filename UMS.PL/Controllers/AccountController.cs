using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UMS.DAL.Models;
using UMS.PL.ViewModels;

namespace UMS.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;

        public AccountController(UserManager<ApplicationUser> userManger)
        {
            _userManger = userManger;
        }

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
                    FullName=registerVM.FullName,
                    Email=registerVM.Email,
                    IsAgree=registerVM.IsAgree
                };

                var result = await _userManger.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                   return RedirectToAction("Login");
                
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(" ", error.Description);
                   

                
            }

            return View(registerVM);


        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginVM)
        {
            return NotFound();
        }

    }
}
