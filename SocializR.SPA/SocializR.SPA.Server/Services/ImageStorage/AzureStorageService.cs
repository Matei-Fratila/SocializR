using Azure.Identity;
using Azure.Storage.Blobs;

namespace SocializR.Services.MediaServices
{
    public class AzureStorageService(IOptionsMonitor<AzureImageStorageSettings> _configuration) : IImageStorage
    {
        private readonly BlobServiceClient _blobClient = new(new Uri(_configuration.CurrentValue.BaseUri), new DefaultAzureCredential());

        public async Task<string> SaveImage(Stream stream, string type)
        {
            var imageId = Guid.NewGuid().ToString().ToLower(); ;
            var containerClient = _blobClient.GetBlobContainerClient(_configuration.CurrentValue.ContainerName);
            try
            {
                var response = await containerClient.UploadBlobAsync(imageId, stream);
                return imageId;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public string UriFor(string imageId)
        {
            return $"{_configuration.CurrentValue.BaseUri}/{_configuration.CurrentValue.ContainerName}/{imageId}";
        }
    }
}
