using System.ComponentModel.DataAnnotations;

namespace BlogWebSite.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen Rol Adı Giriniz!")]
        public string name { get; set; }
    }
}
