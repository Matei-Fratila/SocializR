﻿namespace SocializR.Entities;

public partial class Album : IEntity
{
    public Album()
    {
        Media = new HashSet<Media>();
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Media> Media { get; set; }
}
