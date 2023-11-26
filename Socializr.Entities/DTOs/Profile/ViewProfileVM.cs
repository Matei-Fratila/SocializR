namespace SocializR.Entities.DTOs.Profile;

public class ViewProfileVM
{
    public string Id { get; set; }

    public RelationTypes RelationToCurrentUser { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int MutualFriends { get; set; }

    public string FilePath { get; set; }

    public DateTime? BirthDate { get; set; }

    public string City { get; set; }

    public string County { get; set; }

    public string Gender { get; set; }

    public bool IsPrivate { get; set; }

    public List<AlbumVM> Albums { get; set; }

    public List<string> MyInterests { get; set; }

    public List<SelectListItem> Interests { get; set; }
}
