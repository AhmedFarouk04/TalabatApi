using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public string Password {  get; set; }
    }
}
