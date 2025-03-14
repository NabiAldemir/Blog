using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic4:ViewComponent
    {
        AdminManager am = new AdminManager(new EfAdminRepository());
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = am.GetList().Where(a=>a.AdminID==1).Select(b=>b.Name).FirstOrDefault();
            ViewBag.v2 = am.GetList().Where(a=>a.AdminID==1).Select(b=>b.ImageURL).FirstOrDefault();
            ViewBag.v3 = am.GetList().Where(a=>a.AdminID==1).Select(b=>b.ShortDescription).FirstOrDefault();
            return View();
        }

    }
}
