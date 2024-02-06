using Azure.Storage.Blobs;

namespace RzhadBids.Services
{
    public class PhotoStorageService : IPhotoUploadService
    {
        private readonly BlobServiceClient blobServiceClient;
        private const string ContainerName = "rzhadbidimages";
        public PhotoStorageService(IConfiguration configuration) {
            string? connection = configuration.GetConnectionString("AzureBlobStorage");
            blobServiceClient = new BlobServiceClient(connection);
        }

        public async Task UploadBlobAsync(string blobName, Stream content)
        {
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content, true);
        }
    }
}
