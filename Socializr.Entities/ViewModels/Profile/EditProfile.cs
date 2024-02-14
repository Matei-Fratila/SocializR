namespace SocializR.Models.ViewModels.Profile;

public class EditProfile
{
    public Guid Id { get; set; }
    public IFormFile Avatar { get; set; }

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

    public SelectItem City { get; set; }
    public SelectItem County { get; set; }
    public SelectItem Gender { get; set; }
    public List<SelectItem> Interests { get; set; }

    public bool IsPrivate { get; set; }
}
