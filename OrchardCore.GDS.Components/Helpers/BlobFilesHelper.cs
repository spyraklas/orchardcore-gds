using Microsoft.AspNetCore.Http;
using OrchardCore.GDS.Components.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Helpers
{
    public class BlobFilesHelper : IBlobFilesHelper
    {
        private readonly IBlobFilesHandler _blobFilesHandler;

        public BlobFilesHelper(IBlobFilesHandler blobFilesHandler) 
        {
            _blobFilesHandler = blobFilesHandler;
        }

        public async Task UploadFile(IFormFile file, string fileReference)
        {
            using (var fileMemoryStream = new MemoryStream())
            {
                file.CopyTo(fileMemoryStream);
                var fileBytes = fileMemoryStream.ToArray();
                string[] arrFileName = file.FileName.Split('.');
                string fileExtension = arrFileName[arrFileName.Length - 1].ToLower();
                string fileName = $"File-{fileReference}-{DateTime.Now.ToString("yyyyMMdd-hhmmss")}.{fileExtension}";

                //create document
                BlobFile doc = new()
                {
                    Reference = fileReference,
                    Name = fileName,
                    OriginalName = file.FileName,
                    ContentType = file.ContentType,
                    Content = fileBytes,
                    Size = fileMemoryStream.Length
                };

                //Create meta data dictionary
                Dictionary<string, string> metaDataDictionary = new()
                {
                    { "Reference", fileReference },
                    { "Name", fileName },
                    { "OriginalName", file.FileName },
                    { "ContentType", file.ContentType },
                    { "Size", fileMemoryStream.Length.ToString() }
                };

                //Upload to blob storage
                 await _blobFilesHandler.UploadAsync(doc, metaDataDictionary);
            }
        }
    }
}
