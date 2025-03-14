using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
