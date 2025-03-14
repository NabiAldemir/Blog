using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Blog.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        Context context=new Context();
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var writerID = context.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerID);
            var message = mm.GetList().Where(x=>x.ReceiverID== writerID).Count();
            ViewBag.Message = message;
            return View(values);
        }
    }
}

