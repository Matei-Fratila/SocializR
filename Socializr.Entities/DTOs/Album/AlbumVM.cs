namespace SocializR.Entities.DTOs.Album;

public class AlbumVM
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid CoverId { get; set; }

    public int NrOfImages { get; set; }
}
