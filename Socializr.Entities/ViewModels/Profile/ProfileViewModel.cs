namespace SocializR.Models.ViewModels.Profile;

public class ProfileViewModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateTime? BirthDate { get; set; }

    [Display(Name = "City")]
    public SelectItem City { get; set; }

    [Display(Name = "County")]
    public SelectItem County { get; set; }

    public SelectItem Gender { get; set; }

    public bool IsPrivate { get; set; }

    public List<SelectItem> Interests { get; set; }

    public IFormFile File { get; set; }
}
