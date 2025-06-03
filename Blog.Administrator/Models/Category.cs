using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Administrator.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        public string CategoryTitle { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryDescription { get; set; }
    }
}
