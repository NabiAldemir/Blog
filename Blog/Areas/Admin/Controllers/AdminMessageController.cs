using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Bibliography;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using NuGet.Protocol;

namespace BlogWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        Context c = new Context();
        public IActionResult Inbox()
        {

            var username = User.Identity.Name;
            var writerId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerId);
            ViewBag.v1=c.Messages2.Where(x=>x.ReceiverID == writerId).Count();
            return View(values);
        }
        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var writerId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var values = mm.GetSendBoxListByWriter(writerId);
            ViewBag.v1 = c.Messages2.Where(x => x.ReceiverID == writerId).Count();
            ViewBag.v2 = c.Messages2.Where(x => x.SenderID == writerId).Count();
            return View(values);
        }
        [HttpGet]
        public IActionResult SendMessage()
        {
            var username = User.Identity.Name;
            var writerId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
            var list = (from x in c.Users
                        where x.Id != writerId
                        select new SelectListItem
                        {
                            Text = x.NameSurname,
                            Value = x.Id.ToString()
                        }).ToList().DistinctBy(x => x.Text);
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 m)
        {
            try
            {
                var username = User.Identity.Name;
                var senderId = c.Users.Where(x => x.UserName == username).Select(y => y.Id).FirstOrDefault();
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
        public IActionResult DeleteMessage(int id)
        {
            var value = mm.TGetById(id);
            mm.TDelete(value);
            return Json(new { success = true, message = "Başarılı" });
        }
    }
}
