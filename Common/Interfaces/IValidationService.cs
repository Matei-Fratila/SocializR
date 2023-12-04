using System;

namespace Common.Interfaces
{
    public interface IValidationService
    {
        bool EmailExists(string email);

        bool AlbumExists(string name, Guid id);
    }
}
