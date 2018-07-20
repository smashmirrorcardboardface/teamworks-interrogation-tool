using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetVue.Models;
using dotnetVue.Helpers;


namespace dotnetVue.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IAzureBlobStorage blobStorage;

        public FileController(IAzureBlobStorage blobStorage)
        {
            this.blobStorage = blobStorage;
        }

        public async Task<IActionResult> Index()
        {
            var model = new FilesViewModel();
            foreach (var item in await blobStorage.ListAsync())
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, BlobName = item.BlobName });
            }
            return View(model);
        }

        public async Task<IActionResult> Download(string blobName, string name)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            var stream = await blobStorage.DownloadAsync(blobName);
            return File(stream.ToArray(), "application/octet-stream", name);
        }

        public async Task<IActionResult> Delete(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            await blobStorage.DeleteAsync(blobName);

            return RedirectToAction("Index");
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
