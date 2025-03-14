using System.Net.WebSockets;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.ViewComponents.Writer
{
    public class WriterAboutOnDashboard:ViewComponent
    {
        UserManager um=new UserManager(new EfUserRepository());
        Context context = new Context();
        private readonly UserManager<AppUser> _userManager;

        public WriterAboutOnDashboard(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var username=User.Identity.Name;
            var writerId=context.Users.Where(x=>x.UserName==username).Select(y=>y.Id).FirstOrDefault();
            var values = _userManager.Users.FirstOrDefault(u=>u.Id==writerId);
            return View(values);
        }
    }
}
 