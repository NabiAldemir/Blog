using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EfCategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        Context c = new Context();
        public void DoPassive(int id)
        {
            var category = c.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (category != null)
            {
                category.CategoryStatus = false;
                c.SaveChanges();
            }
        }
    }
}
