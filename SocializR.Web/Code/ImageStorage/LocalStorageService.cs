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
        var uploads = Path.Combine(hostingEnvironment.ContentRootPath, @"images\uploads");
        var imageId = Guid.NewGuid().ToString() + "." + type;
        var filePath = Path.Combine(uploads, imageId);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageStream.CopyToAsync(fileStream);
        }

        return imageId;
    }

    public string UriFor(string imageId)
    {
        return "\\images\\uploads\\" + imageId;
    }
}
