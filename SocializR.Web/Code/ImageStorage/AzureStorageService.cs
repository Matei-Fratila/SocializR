using Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using SocializR.Web.Code.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SocializR.Services.MediaServices
{
    public class AzureStorageService : IImageStorage
    {
        private readonly AzureImageStorageSettings configuration;
        private readonly CloudBlobClient blobClient;

        public AzureStorageService(IOptions<AzureImageStorageSettings> configuration)
        {
            this.configuration = configuration.Value;
            var credentials = new StorageCredentials(this.configuration.AccountName, this.configuration.KeyValue);
            blobClient = new CloudBlobClient(new Uri(this.configuration.BaseUri), credentials);
        }

        public async Task<string> SaveImage(Stream imageStream, string type)
        {
            var imageId = Guid.NewGuid().ToString().ToLower(); ;
            var container = blobClient.GetContainerReference(configuration.ContainerName);
            var blob = container.GetBlockBlobReference(imageId);
            await blob.UploadFromStreamAsync(imageStream);
            return imageId;
        }

        public string UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            var container = blobClient.GetContainerReference(configuration.ContainerName);
            var blob = container.GetBlockBlobReference(imageId);
            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return $"{configuration.BaseUri}{configuration.ContainerName}/{imageId}{sas}";
        }
    }
}
