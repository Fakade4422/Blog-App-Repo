using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Administrator.Models
{
    public class ActiveUser
    {
        public int ActiveUserID { get; set; }
        public int UserID { get; set; }
        public DateTime TimeLoggedIn { get; set; }
        public DateTime TimeLoggedOut { get; set; }
        public DateTime DayLogggedIn { get; set; }
        public float? Duration { get;}
    }
}
