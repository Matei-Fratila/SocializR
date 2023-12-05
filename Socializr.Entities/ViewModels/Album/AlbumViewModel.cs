namespace SocializR.Models.ViewModels.Album;

public class AlbumViewModel : IValidatableObject
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CoverId { get; set; }
    public string CoverFilePath { get; set; }
    public int NrOfImages { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<MediaViewModel> Media { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var result = new List<ValidationResult>();

        var service = validationContext.GetService(typeof(IValidationService)) as IValidationService;
        var albumExists = service.AlbumExists(Name, Id);

        if (albumExists)
        {
            result.Add(new ValidationResult("An album with this name already exists", new List<string> { nameof(Name) }));
        }

        return result;
    }
}
