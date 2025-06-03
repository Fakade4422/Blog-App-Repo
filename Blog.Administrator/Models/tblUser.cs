using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Administrator.Models
{
    public class tblUser
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }

        public IFormFile? File { get; set; }
        public byte[]? Picture { get; set; }

        [Required]
        public DateTime DOB { get; set; }
        [Required]
        
        public string? Password { get; set; }
    }
}
