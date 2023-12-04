namespace SocializR.Models.ViewModels.Interest;

public class EditInterestViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string ParentId { get; set; }

    public List<SelectListItem> Interests;
}
