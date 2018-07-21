using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetVue.Models;
using dotnetVue.Helpers;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace dotnetVue.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private async Task<CloudBlobContainer> GetContainerAsync()
        {
            var configuration = ConfigurationHelper.GetConfiguration(System.IO.Directory.GetCurrentDirectory());
            //Account
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new StorageCredentials(configuration.GetValue<string>("Blob_StorageAccount"), configuration.GetValue<string>("Blob_StorageKey")), false);

            //Client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(configuration.GetValue<string>("Blob_ContainerName"));
            await blobContainer.CreateIfNotExistsAsync();
            //await blobContainer.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });

            return blobContainer;
        }

        private async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
        {
            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            //Blob
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            return blockBlob;
        }


        [HttpGet("[action]")]
        public async Task<List<AzureBlobItem>> GetBlobListAsync(int ticketReference, bool useFlatListing = true)
        {
            //Container
            Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer blobContainer = await GetContainerAsync();

            var list = new List<AzureBlobItem>();
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment =
                    await blobContainer.ListBlobsSegmentedAsync(ticketReference.ToString(), useFlatListing, new BlobListingDetails(), 100, token, null, null);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    list.Add(new AzureBlobItem(item));
                }
            } while (token != null);

            return list.OrderBy(i => i.Folder).ThenBy(i => i.Name).ToList();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Download(string blobName)
        {
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            var stream = await DownloadAsync(blobName);
            return File(stream.ToArray(), "application/octet-stream", blobName);
        }

        public async Task<MemoryStream> DownloadAsync(string blobName)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Download
            using (var stream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(stream);
                return stream;
            }
        }

    }
    public class FileDetails
    {
        public string Name { get; set; }
        public string BlobName { get; set; }
    }

    public class FilesViewModel
    {
        public List<FileDetails> Files { get; set; }
            = new List<FileDetails>();
    }
}
