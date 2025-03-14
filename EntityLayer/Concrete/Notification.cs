using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public string NotificationtType { get; set; }
        public string NotificationtTypeSymbol { get; set; }
        public string NotificationtDetails { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool NotificationStatus{ get; set; }
        public string NotificationColor{ get; set; }
        

    }
}
