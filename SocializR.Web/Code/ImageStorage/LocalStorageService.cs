using Microsoft.AspNetCore.Hosting;

namespace SocializR.Services.MediaServices;

public class LocalStorageService(IWebHostEnvironment _webHostEnvironment,
    IOptionsMonitor<AppSettings> _configuration) : IImageStorage
{
    public async Task<string> SaveImage(Stream imageStream, string type)
    {
        var basePath = Path.Combine(_webHostEnvironment.WebRootPath, _configuration.CurrentValue.FileUploadWriteLocation);
        var name = Guid.NewGuid().ToString() + "." + type;
        var filePath = Path.Combine(basePath, name);

        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }
        }
        catch(Exception ex)
        {

        }

        return name;
    }

    public string UriFor(string imageId)
    {
        return Path.Combine(_configuration.CurrentValue.FileUploadLocation, imageId);
    }
}
