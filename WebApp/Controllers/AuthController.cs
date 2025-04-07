using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController(UserService userService, SignInManager<AppUser> signInManager) : Controller
    {
        private readonly UserService _userService = userService;
        private readonly SignInManager<AppUser> _signInManager =  signInManager;

        public IActionResult Login()
        {
            //return LocalRedirect("/projects");
            return View();
        }


        public async Task<IActionResult> SignUp(UserSignUpForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var result = await _userService.CreateAsync(form);

            if (result)
            {
                return Json(new { success = true, redirectUrl = Url.Action("SignIn", "Auth") });
            }

            else
            {
                return View(form);
            }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Projects", "Admin");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInForm form)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
                if (result.Succeeded)
                {
                    Console.WriteLine("Login successful");
                    //return RedirectToAction("members", "Admin");
                    return Json( new { success = true, redirectUrl = Url.Action("Members", "Admin") });
                }
            }

            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View(form);
        }

        public new async Task <IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
