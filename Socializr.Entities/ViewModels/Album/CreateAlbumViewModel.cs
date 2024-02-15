namespace Socializr.Models.ViewModels.Album;
public class CreateAlbumViewModel : IValidatableObject
{
    [Required]
    public string Name { get; set; }
    public string[] Captions { get; set; }
    public IFormFile[] Files { get; set; }
    public Guid UserId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var result = new List<ValidationResult>();

        var service = validationContext.GetService(typeof(IValidationService)) as IValidationService;

        var albumExists = service.AlbumExists(Name, UserId);

        if (albumExists)
        {
            result.Add(new ValidationResult("An album with this name already exists", new List<string> { nameof(Name) }));
        }

        return result;
    }
}
