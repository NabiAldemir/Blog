using System.Xml.Linq;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace BlogWebSite.ViewComponents.Blog
{
    public class LastAddedPost:ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var value = bm.GetList().OrderByDescending(x => x.BlogCreateDate).Take(1);
            return View(value);
        }
    }
}
