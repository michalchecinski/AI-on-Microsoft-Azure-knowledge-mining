using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using TechnicalDocuIndexer.Web.Models;

namespace TechnicalDocuIndexer.Web.Service
{
    using System.IO;
    
    public class AzureStorageFileRepository : IFileRepository
    {
        private readonly string _storageAccountName;
        
        public AzureStorageFileRepository(IOptions<ConnectionConfigurationModel> connectionConfigurationModel)
        {
            _storageAccountName = connectionConfigurationModel.Value.StorageAccountName;
        }

        public async Task<byte[]> DownloadFileContentAsync(string fileUrl)
        {
            var blobClient = new BlobClient(new Uri(fileUrl), new DefaultAzureCredential());
            
            var download = await blobClient.DownloadAsync();

            await using var stream = new MemoryStream();
            
            await download.Value.Content.CopyToAsync(stream);
            
            return stream.ToArray();
        }
        
        public async Task<byte[]> DownloadFileContentAsync(string containerName, string fileName)
        {
            var containerClient = await GetBlobContainerClientAsync(containerName);
            
            var blobClient = containerClient.GetBlobClient(fileName);
            
            var download = await blobClient.DownloadAsync();
        
            await using var stream = new MemoryStream();
            
            await download.Value.Content.CopyToAsync(stream);
            
            return stream.ToArray();
        }
        
        public async Task UploadFileAsync(string containerName, string fileName, Stream fileContent)
        {
            var containerClient = await GetBlobContainerClientAsync(containerName);
        
            var blobClient = containerClient.GetBlobClient(fileName);
            
            await blobClient.UploadAsync(fileContent, true);
        }
        
        private async Task<BlobContainerClient> GetBlobContainerClientAsync(string containerName)
        {
            var containerEndpoint = $"https://{_storageAccountName}.blob.core.windows.net/{containerName}";
        
            var containerClient = new BlobContainerClient(new Uri(containerEndpoint),
                new DefaultAzureCredential());
        
            await containerClient.CreateIfNotExistsAsync();
            
            return containerClient;
        }
    }
}