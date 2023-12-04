namespace SocializR.Models.ViewModels.Album;

public class CreateAlbumViewModel : IValidatableObject
{
    public bool IsDataInvalid { get; set; }

    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var result = new List<ValidationResult>();

        var service = validationContext.GetService(typeof(IValidationService)) as IValidationService;
        var albumExists = service.AlbumExists(Name, Id);

        if (albumExists)
        {
            result.Add(new ValidationResult("Numele exista deja", new List<string> { nameof(Name) }));
        }

        return result;
    }
}
