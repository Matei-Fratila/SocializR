namespace SocializR.Entities.DTOs.Profile;

public class ProfileVM
{
    public bool IsDataInvalid { get; set; }

    public Guid Id { get; set; }

    public string FilePath { get; set; }

    [Required(ErrorMessage = "Campul este obligatoriu!")]
    [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Campul este obligatoriu!")]
    [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateTime? BirthDate { get; set; }

    [Display(Name = "City")]
    public Guid CityId { get; set; }

    public List<SelectListItem> Cities { get; set; }

    [Display(Name = "County")]
    public Guid CountyId { get; set; }

    public List<SelectListItem> Counties { get; set; }

    public GenderTypes Gender { get; set; }

    public bool IsPrivate { get; set; }

    [Display(Name = "Interests")]
    public List<string> MyInterests { get; set; }

    public List<SelectListItem> Interests { get; set; }

    public IFormFile ProfilePhoto { get; set; }
}
