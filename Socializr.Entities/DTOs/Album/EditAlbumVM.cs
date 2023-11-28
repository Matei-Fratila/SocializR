namespace SocializR.Entities.DTOs.Album;

public class EditAlbumVM 
{
    public bool IsDataInvalid { get; set; }

    public List<MediaModel> Media { get; set; }

    public string Id { get; set; }

    public string UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}
