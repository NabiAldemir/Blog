using System.Xml.Linq;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1:ViewComponent
    {
        BlogManager bm=new BlogManager(new EfBlogRepository());
        Context context = new Context();
        private readonly UserManager<AppUser> _userManager;

        public Statistic1(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var writerid = _userManager.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            ViewBag.v1=bm.GetList().Count();
            ViewBag.v2 = context.Messages2.Where(x => x.ReceiverID == writerid).Count();
            ViewBag.v3=context.Comments.Count();

            string api = "a77af526787c364a4a014176fff03500";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&units=metric&appid=" + api;
            XDocument document=XDocument.Load(connection);
            ViewBag.v4 = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View();
        }
    }
}
