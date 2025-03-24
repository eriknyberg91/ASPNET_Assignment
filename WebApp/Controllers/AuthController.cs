using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {

        public IActionResult Login()
        {
            return LocalRedirect("/projects"); // For redirecting to projects while working on layout
            //return View();
        }
    }
}
