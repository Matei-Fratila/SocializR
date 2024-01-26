using SocializR.Models.ViewModels.Feed;

namespace SocializR.Models.ViewModels.Profile;

public class ViewProfileViewModel
{
    public Guid Id { get; set; }

    public RelationTypes RelationToCurrentUser { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public string DisplayName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public int MutualFriends { get; set; }

    public string FilePath { get; set; }

    [Display(Name = "Date of birth")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime? BirthDate { get; set; }

    public string City { get; set; }

    public string County { get; set; }

    public string Gender { get; set; }

    public bool IsPrivate { get; set; }

    public List<AlbumViewModel> Albums { get; set; }

    public List<Guid> Interests { get; set; }

    public int NrOfFriends { get; set; }

    public int NrOfPhotos { get; set; }

    public int NrOfPosts { get; set; }

    public string Description { get; set; }

    public List<PostViewModel> Posts { get; set; }
}
