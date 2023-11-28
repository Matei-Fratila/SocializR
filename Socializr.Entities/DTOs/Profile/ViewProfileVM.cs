namespace SocializR.Entities.DTOs.Profile;

public class ViewProfileVM
{
    public Guid Id { get; set; }

    public RelationTypes RelationToCurrentUser { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public int MutualFriends { get; set; }

    public string FilePath { get; set; }

    [Display(Name = "Date of birth")]
    public DateTime? BirthDate { get; set; }

    public string City { get; set; }

    public string County { get; set; }

    public string Gender { get; set; }

    public bool IsPrivate { get; set; }

    public List<AlbumVM> Albums { get; set; }

    [Display(Name = "Interests")]
    public List<Guid> MyInterests { get; set; }

    public List<SelectListItem> Interests { get; set; }
}
