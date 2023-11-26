namespace SocializR.Entities.DTOs.Profile;

public class ProfileVM
{
    public bool IsDataInvalid { get; set; }

    public string Id { get; set; }

    public string FilePath { get; set; }

    [Required(ErrorMessage = "Campul este obligatoriu!")]
    [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Campul este obligatoriu!")]
    [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    public string CityId { get; set; }

    public List<SelectListItem> Cities { get; set; }

    public string CountyId { get; set; }

    public List<SelectListItem> Counties { get; set; }

    public GenderTypes Gender { get; set; }

    public bool IsPrivate { get; set; }

    public List<string> MyInterests { get; set; }

    public List<SelectListItem> Interests { get; set; }

    public IFormFile ProfilePhoto { get; set; }
}
