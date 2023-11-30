namespace SocializR.Services.MediaServices;

public class LocalStorageService(IHostEnvironment _hostingEnvironment,
    IOptionsMonitor<AppSettings> _configuration) : IImageStorage
{
    public async Task<string> SaveImage(Stream imageStream, string type)
    {
        var basePath = Path.Combine(_hostingEnvironment.ContentRootPath, _configuration.CurrentValue.FileUploadLocation);
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
        return Path.Combine(_configuration.CurrentValue.FileUploadLocation, imageId);
    }
}
