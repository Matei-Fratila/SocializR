using SocializR.Models.ViewModels.Feed;
using SocializR.Models.ViewModels.Interest;
using SocializR.Models.ViewModels.Map;

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

    public string UserPhoto { get; set; }

    [Display(Name = "Date of birth")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime? BirthDate { get; set; }

    public SelectItem City { get; set; }

    public SelectItem County { get; set; }

    public SelectItem Gender { get; set; }

    public bool IsPrivate { get; set; }

    public List<AlbumViewModel> Albums { get; set; }

    public List<SelectItem> Interests { get; set; }

    public int NrOfFriends { get; set; }

    public int NrOfPhotos { get; set; }

    public int NrOfPosts { get; set; }

    public string Description { get; set; }

    //public List<PostViewModel> Posts { get; set; }
}
