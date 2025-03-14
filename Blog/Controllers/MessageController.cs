
using Azure.Identity;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWebSite.Controllers
{
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        UserManager um = new UserManager(new EfUserRepository());
        Context context = new Context();
        public IActionResult InBox()
        {
            var username = User.Identity.Name;
            var writerID = context.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();

            var list = (from x in context.Users
                        where x.Id != writerID
                        select new SelectListItem
                        {
                            Text = x.NameSurname,
                            Value = x.Id.ToString()
                        }).ToList().DistinctBy(x=>x.Text);
            ViewBag.list = list;
            var values = mm.GetInboxListByWriter(writerID);
            return View(values);
        }
        [HttpGet]
        public IActionResult MessageDetails(int id)
        {
            var value = mm.TGetById(id);
            return View(value);
        }
        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 m)
        {
            try
            {
                var username = User.Identity.Name;
                var senderId = context.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
                m.MessageDate = DateTime.Now;
                m.MessageStatus = true;
                m.SenderID = senderId;
                mm.TAdd(m);
                return Json(new { success = true, message = "Başarılı" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Başarısız" });
            }
        }
    }
}
