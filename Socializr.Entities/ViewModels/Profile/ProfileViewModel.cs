namespace SocializR.Models.ViewModels.Profile;

public class ProfileViewModel
{
    public bool IsDataInvalid { get; set; }

    public Guid Id { get; set; }

    public string FileName { get; set; }

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
    public Guid? CityId { get; set; }

    [Display(Name = "County")]
    public Guid? CountyId { get; set; }

    public GenderTypes Gender { get; set; }

    public bool IsPrivate { get; set; }

    public List<Guid> Interests { get; set; }

    public IFormFile ProfilePhoto { get; set; }
}
