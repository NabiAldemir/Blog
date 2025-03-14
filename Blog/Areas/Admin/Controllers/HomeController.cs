using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var username = User.Identity.Name;
            ViewBag.name = c.Users.Where(x => x.UserName == username).Select(y => y.NameSurname).FirstOrDefault();
            return View();
        }
    }
}
