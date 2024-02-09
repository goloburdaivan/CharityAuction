namespace RzhadBids.Services
{
    public interface IPhotoUploadService
    {
        public Task UploadBlobAsync(string blobName, Stream content);
    }
}
