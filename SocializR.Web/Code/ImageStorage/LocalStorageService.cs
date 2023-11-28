namespace SocializR.Services.MediaServices;

public class LocalStorageService : IImageStorage
{
    private readonly IHostEnvironment hostingEnvironment;

    public LocalStorageService(IHostEnvironment hostingEnvironment)
    {
        this.hostingEnvironment = hostingEnvironment;
    }

    public async Task<string> SaveImage(Stream imageStream, string type)
    {
        var basePath = Path.Combine(hostingEnvironment.ContentRootPath, @"wwwroot\images\uploads");
        var name = Guid.NewGuid().ToString() + "." + type;
        var filePath = Path.Combine(basePath, name);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageStream.CopyToAsync(fileStream);
        }

        return name;
    }

    public string UriFor(string imageId)
    {
        return @$"~/images/uploads/{imageId}";
    }
}
