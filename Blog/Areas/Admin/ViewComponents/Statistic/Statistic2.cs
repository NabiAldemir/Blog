using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2:ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1=context.Blogs.OrderByDescending(x=>x.BlogID).Select(x=>x.BlogTitle).Take(1).FirstOrDefault();
            ViewBag.v2 = context.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogImage).Take(1).FirstOrDefault();
            return View();
        }
    }
}
