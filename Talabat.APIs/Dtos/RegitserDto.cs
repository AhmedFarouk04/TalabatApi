using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class RegitserDto
    {

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber   { get; set; }

        public string Password { get; set; }



    }
}
