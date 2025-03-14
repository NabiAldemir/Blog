using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Concrete;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Concrete;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        Context context = new Context();


        [AllowAnonymous]
        public IActionResult Index(EntityLayer.Concrete.Blog p)
        {
            var username = User.Identity.Name;
            var writerName=context.Users.Where(x=>x.UserName== username).Select(y=>y.NameSurname).FirstOrDefault();
            ViewBag.writerName = writerName;
            var values = bm.GetBlogListWithCategory();
            ViewBag.IsLoggedIn = User.Identity.IsAuthenticated;
            ViewBag.v = context.Comments.Where(x => x.BlogID == p.BlogID).Count();
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.Id = id;
            var values = bm.GetBlogByID(id);
            return View(values);
        }
        public ActionResult BlogListByWriter()
        {
            var username = User.Identity.Name;
            var writerID= context.Users.Where(x=>x.UserName== username).Select(y=>y.Id).FirstOrDefault();
            List<SelectListItem> list = (from x in cm.GetList()
                                         select new SelectListItem
                                         {
                                             Text = x.CategoryName,
                                             Value = x.CategoryID.ToString()
                                         }).ToList();
            ViewBag.list = list;
            var value = bm.GetBlogListWithCategoryWriter(writerID);
            return View(value);
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBlog(EntityLayer.Concrete.Blog p)
        {
            var username = User.Identity.Name;
            var writerID = context.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            try
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Now;
                p.WriterID = writerID;
                bm.TAdd(p);

                // 🟢 Başarılı yanıt
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // ❌ Hata durumunda false dön
                return Json(new { success = false, message = ex.Message });
            }

        }
        [HttpGet]
        public IActionResult DeleteBlog(int id)
        {
            var blogvalue=bm.TGetById(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            List<SelectListItem> list = (from x in cm.GetList()
                                         select new SelectListItem
                                         {
                                             Text = x.CategoryName,
                                             Value = x.CategoryID.ToString()
                                         }).ToList();
            ViewBag.list = list;
            var blogvalue=bm.TGetById(id);
            return Json(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(EntityLayer.Concrete.Blog p)
        {
            var username = User.Identity.Name;
            var writerID = context.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            try
            {
                var blog=bm.TGetById(p.BlogID);
                p.BlogStatus = true;
                p.WriterID=writerID;
                p.BlogCreateDate = blog.BlogCreateDate;
                bm.TUpdate(p);
                return Json(new { success = true, message = "Blog başarıyla güncellendi!" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
