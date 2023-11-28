namespace SocializR.Entities.DTOs.Account;

public class LoginVM
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    public string Password { get; set; }

    public bool AreCredentialsInvalid { get; set; }

    public string ReturnUrl { get; set; }
}
