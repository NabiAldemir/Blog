using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller

    {
        CommentManager cm = new CommentManager(new EfCommentRepository());
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            var values = bm.GetList().Select(x=>x.BlogID).FirstOrDefault();
            return PartialView(values);
        }
        [HttpPost]
        public JsonResult PartialAddComment(Comment c)
        {
            try
            {
                if (c == null)
                {
                    return Json(new { success = false, message = "Gönderilen veri boş!" });
                }

                c.CommentDate = DateTime.Now;
                c.CommentStatus = true;

                cm.TAdd(c); 

                return Json(new { success = true, message = "Yorum eklendi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }

        public PartialViewResult CommentListByBlog(int id)
        {
            var values=cm.GetList(id);
            return PartialView(values);
        }
    }
}
