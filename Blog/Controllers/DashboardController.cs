using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class DashboardController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        { 
            ViewBag.v1=c.Blogs.Count().ToString();
            var username = User.Identity.Name;
            var writerID=c.Users.Where(x=>x.UserName== username).Select(y=>y.Id).FirstOrDefault();
            var writername = c.Users.Where(x => x.UserName == username).Select(y => y.NameSurname).FirstOrDefault();
            ViewBag.v2=c.Blogs.Where(x=>x.WriterID==writerID).Count();
            ViewBag.writerName = writername;
            ViewBag.v3=c.Categories.Count().ToString();
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View();  
        }
    }
}
