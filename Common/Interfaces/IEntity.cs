using System;

namespace Common.Interfaces;

public interface IEntity
{
    DateTime CreatedDate { get; set; }
    DateTime? LastModifiedDate { get; set; }
    bool IsDeleted { get; set; }
    byte[] TimeStamp { get; set; }
}
