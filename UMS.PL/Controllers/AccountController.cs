using Microsoft.AspNetCore.Mvc;
using UMS.PL.ViewModels;

namespace UMS.PL.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerVM )
        {
            return NotFound();
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
