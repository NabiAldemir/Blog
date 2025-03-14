using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Extensions;

namespace BlogWebSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        Context c = new Context();
        public IActionResult Index()
        {
            var values = cm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category c)
        {
            try
            {
                c.CategoryStatus = true;
                cm.TAdd(c);
                return Json(new { success = true, message = "Başarılı" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Başarısız" });
            }

        }
        [HttpPost]
        public IActionResult DoPassive(int id)
        {
            cm.DoCategoryPassive(id);
            return Json(new { success = true, message = "Başarılı" });
        }
        [HttpPost]
        public IActionResult DoActive(int id)
        {
            var category = c.Categories.FirstOrDefault(x => x.CategoryID == id);
            if (category != null)
            {
                category.CategoryStatus = true;
                c.SaveChanges();
            }
            return Json(new { success = true, message = "Başarılı" });
        }
        public IActionResult Delete(int id) 
        { 
            var value= cm.TGetById(id);
            cm.TDelete(value);
            return Json(new {success=true , message="Başarılı"});
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var value = cm.TGetById(id);
            return Json(value);
        }
        [HttpPost]
        public IActionResult Update(Category c)
        {
            var values = cm.TGetById(c.CategoryID);
            c.CategoryStatus = true;
            cm.TUpdate(c);
            return Json(new { success = true, message = "Başarılı" });
        }
    }
}
