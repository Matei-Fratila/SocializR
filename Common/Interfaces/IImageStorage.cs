namespace Common.Interfaces;

public interface IImageStorage
{
    Task<string> SaveImage(Stream imageStream, string type);

    string UriFor(string imageId);
}
