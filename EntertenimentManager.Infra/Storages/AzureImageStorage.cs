using Azure.Storage.Blobs;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Queries;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using System;
namespace EntertenimentManager.Infra.Storages
{
    public class AzureImageStorage : IImageStorage
    {
        private readonly string _azurestorageConnectionString;
        private readonly string _container;
        public AzureImageStorage(string azurestorageConnectionString, string container)
        {
            _azurestorageConnectionString = azurestorageConnectionString;
            _container = container;
        }
        public void Upload(byte[] imageBytes, string fileName)
        {
            var blobClient = new BlobClient(_azurestorageConnectionString, _container, fileName);

            using var stream = new MemoryStream(imageBytes);
            blobClient.UploadAsync(stream);
        }
    }
}
