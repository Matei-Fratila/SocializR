namespace SocializR.Entities.DTOs.Album;

public class CreateAlbumVM : IValidatableObject
{
    public bool IsDataInvalid { get; set; }

    public string Id { get; set; }

    [Required(ErrorMessage = "Campul este obligatoriu!")]
    [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
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
