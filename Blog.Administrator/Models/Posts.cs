using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Administrator.Models
{
    public class Posts
    {
        [Key]
        public int PostID { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostBody { get; set; }
        [Required]
        public bool Featured { get; set; }
        public IFormFile? File { get; set; }
        [Required]
        public byte[]? Thumbnail { get; set; }

        [ForeignKey("CategoryID")]
        [Required]
        public int? CategoryID { get; set; }
        public string? CategoryTitle { get; set; }

    }
}
