using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Auth.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Enter your Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your Surname")]
        [StringLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter your Username")]
        [StringLength(20)]
        public string? Username { get; set; }

        [Required]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression("^[a-z0-9]+@[a-zA-Z0-9]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Example. johndoe06@gmail.com. Must " +
        "contain @ sign after local name and end with . followed by top-level domain ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Select your Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Select an Image")]
        public IFormFile? File { get; set; }

        [Required(ErrorMessage = "Select an Image")]
        public byte[]? Picture { get; set; }

        [Required(ErrorMessage = "Select your date of Birth")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [RegularExpression(@"^(?=(?:.*[a-z]){2,})(?=.*[A-Z])(?=.*[\W_]).{6,}$",
        ErrorMessage = "Password must have at least 6 characters, 1 uppercase, 2 lowercase letters, and a symbol.")]
        public string? Password { get; set; }
    }
}
