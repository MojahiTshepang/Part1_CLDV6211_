using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System;

namespace SlaezyBookingEventApp.Services
{
    public class AzureBlobImageService : IImageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        public AzureBlobImageService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage"));
            _blobContainerName = configuration["AzureBlobStorageContainerName"];
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string imageName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            await blobContainerClient.CreateIfNotExistsAsync(); // Ensure the container exists

            var blobClient = blobContainerClient.GetBlobClient(imageName);
            await blobClient.UploadAsync(imageStream);

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return false;
            }

            try
            {
                Uri uri = new Uri(imageUrl);
                string blobName = System.IO.Path.GetFileName(uri.LocalPath);

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                var response = await blobClient.DeleteIfExistsAsync();
                return response.Value; // Returns true if the blob existed and was deleted
            }
            catch (UriFormatException)
            {
                // Handle invalid URL format
                return false;
            }
            catch (Exception)
            {
                // Handle other potential exceptions (e.g., network issues, Azure errors)
                return false;
            }
        }
    }
}
