using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog.ViewComponents.Blog
{
    public class WritersBlog:ViewComponent
    {
        BlogManager bm=new BlogManager(new EfBlogRepository());
        Context context=new Context();
        public IViewComponentResult Invoke()
        {
            var usermail = User.Identity.Name;
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = bm.GetBlogListForWriter(writerID);
            return View(values);
        }
    }
}
