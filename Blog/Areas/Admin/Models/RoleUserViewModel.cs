using EntityLayer.Concrete;

namespace BlogWebSite.Areas.Admin.Models
{
    public class RoleUserViewModel
    {
        public List<AppRole> Roles { get; set; }
        public List<AppUser> Users { get; set; }
    }
}
