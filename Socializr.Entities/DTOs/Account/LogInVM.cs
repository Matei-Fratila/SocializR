using System.ComponentModel.DataAnnotations;

namespace SocializR.Entities.DTOs.Account
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool AreCredentialsInvalid { get; set; }

        public string ReturnUrl { get; set; }
    }
}
