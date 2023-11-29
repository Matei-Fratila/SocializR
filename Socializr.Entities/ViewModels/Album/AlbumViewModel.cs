namespace SocializR.Models.ViewModels.Album;

public class AlbumViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid CoverId { get; set; }

    public string CoverFilePath { get; set; }

    public int NrOfImages { get; set; }
}
