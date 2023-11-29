namespace SocializR.Models.ViewModels.Account;

public class RegisterViewModel : IValidatableObject
{
    [MaxLength(100)]
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [MaxLength(100)]
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Adresa nu este valida!")]
    [MaxLength(100)]
    [Required]
    //[Remote(action: "IsEmailAvailable", controller: "Account", ErrorMessage = "Email-ul exista deja")]
    public string Email { get; set; }

    [MaxLength(100)]
    [Required]
    public string Password { get; set; }

    [Display(Name = "City")]
    public string CityId { get; set; }
    public List<SelectListItem> Cities { get; set; }

    [Display(Name = "County")]
    public string CountyId { get; set; }
    public List<SelectListItem> Counties { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateTime? BirthDate { get; set; }

    public int Gender { get; set; }

    [Display(Name = "Privacy level")]
    public bool IsPrivate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var result = new List<ValidationResult>();

        var service = validationContext.GetService(typeof(IValidationService)) as IValidationService;
        var emailExists = service.EmailExists(Email);

        if (emailExists)
        {
            result.Add(new ValidationResult("Email-ul exista deja", new List<string> { nameof(Email) }));
        }

        return result;
    }
}
