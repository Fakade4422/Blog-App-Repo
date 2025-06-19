using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Administrator.Models
{
    public class Posts
    {
        [Key]
        public int PostID { get; set; }
        [Required(ErrorMessage = "The Post Title is required")]
        public string PostTitle { get; set; }
        [Required(ErrorMessage = "The Post Body is required")]
        public string PostBody { get; set; }
        [Required]
        public bool Featured { get; set; }
        [Required(ErrorMessage = "Thumbnail file is required.")]
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public byte[]? Thumbnail { get; set; }

        [ForeignKey("CategoryID")]
        [Required]
        public int? CategoryID { get; set; }
        public string? CategoryTitle { get; set; }

    }
}
