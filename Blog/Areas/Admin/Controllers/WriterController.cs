using BlogWebSite.Areas.Admin.Models;
using BlogWebSite.Models;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogWebSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class WriterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        UserManager um = new UserManager(new EfUserRepository());
        Context c = new Context();

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = um.GetList();
            return View(values);
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var values = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
                if (values == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı!" });
                }

                var result = await _userManager.DeleteAsync(values);

                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Kullanıcı başarıyla silindi!" });
                }
                return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Sunucu hatası: " + ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(AppUser p)
        {
            try
            {
                var values = _userManager.Users.Where(x => x.Id == p.Id).FirstOrDefault();
                if (values == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı!" });
                }
                values.NameSurname = p.NameSurname;
                values.Email = p.Email;
                values.UserName = p.UserName;
                var result = await _userManager.UpdateAsync(values);

                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Kullanıcı başarıyla güncellendi!" });
                }
                return Json(new { success = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Sunucu hatası: " + ex.Message });
            }
        }
    }
}
