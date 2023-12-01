﻿namespace SocializR.Models.ViewModels.Account;

public class LogInViewModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; }
}
