using BlogWebSite.Areas.Admin.Models;
using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
                }
            }
        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> bm = new List<BlogModel>()
            {
                new BlogModel { ID = 1, BlogName = "c# PROGRAMLAMAYA GİRİŞ" },
                new BlogModel { ID = 2, BlogName = "TESLA FİRMASI ARAÇLARI" },
                new BlogModel { ID = 3, BlogName = "2020 OLİMPİYATLARI" }
            };
            return bm;
        }
        public IActionResult BlogListExcel()
        {
            return View();
        }
        public List<BlogModel> BlogTitleList()
        {
            List<BlogModel> bm = new List<BlogModel>();
            using (var c = new Context())
            { 
                bm=c.Blogs.Select(b => new BlogModel 
                { 
                    ID=b.BlogID,
                    BlogName=b.BlogTitle,
                }).ToList();
            }
            return bm;
        }
    }
}
