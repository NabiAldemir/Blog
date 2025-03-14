
using BlogWebSite.Areas.Admin.Models;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ChartController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryChart()
        {
            using (var context = new Context()) 
            {
                var categoryList = context.Categories.ToList();
                var blogList = bm.GetList().ToList();
                var list = categoryList.Select(c => new CategoryClass
                {
                    categoryname = c.CategoryName,
                    categorycount = blogList.Count(x => x.CategoryID == c.CategoryID) 
                }).ToList();

                return Json(new { jsonlist = list });
            }
        }
    }
}

