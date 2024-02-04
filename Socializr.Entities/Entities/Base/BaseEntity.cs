using System.ComponentModel.DataAnnotations.Schema;

namespace Socializr.Models.Entities.Base;
public class BaseEntity : ISoftDeletable, IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }

    [Timestamp]
    public byte[] TimeStamp { get; set; }
}
