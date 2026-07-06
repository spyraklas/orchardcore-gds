using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.GDS.Components.Config;
using OrchardCore.GDS.Components.Models;
using OrchardCore.Workflows.Helpers;

namespace OrchardCore.GDS.Components.Handlers
{
    public class BlobFilesHandler : IBlobFilesHandler
    {
        private readonly BlobSettings _blobSettings;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<BlobFilesHandler> _logger;

        public BlobFilesHandler(IOptions<BlobSettings> blobSettings, BlobServiceClient blobServiceClient, ILogger<BlobFilesHandler> logger)
        {
            _blobSettings = blobSettings.Value;
            _blobServiceClient = blobServiceClient;
            _logger = logger;

        }
        public async Task<BlobUploadResult> UploadAsync(BlobFile file, Dictionary<string, string> metaDataDictionary)
        {
            BlobUploadResult result = new()
            {
                Message = string.Empty,
                Result = false,
                Status = 0
            };

            BlobClient blobClient;
            string path = PathCombine(_blobSettings.TenantFolder, file.Reference);
            string blobClientPath = PathCombine(path, file.Name);
            metaDataDictionary.Add("PathWithName", blobClientPath);
            try
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobSettings.Container.ToLower());

                //Check if blob container exists
                if (!await containerClient.ExistsAsync())
                {
                    //Create blob
                    var folderCreateInfo = await containerClient.CreateAsync();

                    //Check if blob created or return an error
                    if (folderCreateInfo != null && folderCreateInfo.GetRawResponse().Status != 201 && folderCreateInfo.GetRawResponse().ReasonPhrase.ToLower() != "created")
                    {
                        result.Status = folderCreateInfo.GetRawResponse().Status;
                        result.Message = folderCreateInfo.GetRawResponse().ReasonPhrase;
                        return result;
                    }
                }

                blobClient = containerClient.GetBlobClient(blobClientPath);
            }
            catch (Exception ex)
            {
                string errorMessage = $@"Fail creating ""{_blobSettings.Container.ToLower()}"" or blob client path ""{blobClientPath}"".";
                _logger.LogError(ex, errorMessage);
                result.Message = errorMessage;

                return result;
            }

            try
            {
                using (MemoryStream uploadStream = new MemoryStream(file.Content))
                {
                    var uploadResult = await blobClient.UploadAsync(uploadStream, null, metaDataDictionary);
                    uploadStream.Close();

                    var fileCreateInfo = uploadResult.GetRawResponse();
                    if (fileCreateInfo.Status == 201)
                    {
                        result.Status = fileCreateInfo.Status;
                        result.Message = fileCreateInfo.ReasonPhrase;
                        result.Result = true;
                        return result;
                    }
                }
            }
            catch (RequestFailedException e)
            {
                if (e.Status.Equals(409))
                {
                    result.Status = e.Status;
                    result.Message = "File exist. Remove it first or try another";
                    return result;
                }
                else
                {
                    result.Status = e.Status;
                    result.Message = $@"Upload failed for blob client path ""{blobClientPath}"".";
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $@"Upload failed for blob client path ""{blobClientPath}"".";
                _logger.LogError(ex, errorMessage);
            }

            return result;
        }

        public async Task<List<BlobFile>> GetStoredFilesAsync(string fileReference)
        {
            var result = new List<BlobFile>();

            try
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobSettings.Container);

                var preExistResult = await containerClient.ExistsAsync();

                if (preExistResult.Value)
                {
                    //containerClient.GetBlobsByHierarchy();

                    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(new GetBlobsOptions() { Traits = BlobTraits.Metadata }))
                    {
                        if (blobItem.Metadata != null && blobItem.Metadata.TryGetValue("Reference", out string referenceValue))
                        {
                            if(!string.IsNullOrEmpty(referenceValue) && referenceValue == fileReference)
                            {
                                
                                result.Add(new BlobFile()
                                {
                                    Reference = referenceValue,
                                    PathWithName = blobItem.Metadata.GetValue("PathWithName"),
                                    Name = blobItem.Metadata.GetValue("Name"),
                                    OriginalName = blobItem.Metadata.GetValue("OriginalName"),
                                    ContentType = blobItem.Metadata.GetValue("ContentType"),
                                    Size = Convert.ToInt64(blobItem.Metadata.GetValue("Size"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to get stored files from Blog.");
            }

            return result;
        }

        public async Task<BlobFile> DownloadAsync(string fileWithPath)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobSettings.Container);
            BlobClient blobClient = containerClient.GetBlobClient(fileWithPath);

            var existResult = await blobClient.ExistsAsync();

            BlobFile blobFile = new();

            if (existResult.Value)
            {
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                IDictionary<string, string> metadata = await GetBlobFileMetadata(fileWithPath);

                using (MemoryStream stream = new MemoryStream())
                {
                    await download.Content.CopyToAsync(stream);

                    blobFile.Content = stream.ToArray();
                    blobFile.ContentType = download.ContentType;
                    blobFile.PathWithName = metadata.GetValue("PathWithName");
                    blobFile.OriginalName = metadata.GetValue("OriginalName");
                    blobFile.Name = metadata.GetValue("Name");
                    blobFile.Reference = metadata.GetValue("Reference");
                    blobFile.Size = Convert.ToInt64(metadata.GetValue("Size"));

                    stream.Close();
                }
            }
            else
            {
                _logger.LogError($@"Application fail to download file ""{fileWithPath}"" from blob storage. The file doesn't exist.");
                return null;
            }

            return blobFile;
        }

        public async Task<bool> DeleteAsync(string fileWithPath)
        {
            bool result = false;

            try
            {
                //Delete PreBlob file
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobSettings.Container);
                BlobClient blobClient = containerClient.GetBlobClient(fileWithPath);

                var existResult = await blobClient.ExistsAsync();

                if (existResult)
                {
                    var deleteResult = await blobClient.DeleteAsync();

                    if (deleteResult.Status == 202 && deleteResult.ReasonPhrase.ToLower() == "accepted")
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to delete file from Blog.");
                return false;
            }

            return result;
        }


        private string PathCombine(string folder, string filename)
        {
            if (!string.IsNullOrWhiteSpace(folder) && !folder.EndsWith("/"))
            {
                folder = folder + "/";
            }
            return folder + filename;
        }

        private async Task<IDictionary<string, string>> GetBlobFileMetadata(string filepath)
        {
            try
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_blobSettings.Container);

                var preExistResult = await containerClient.ExistsAsync();

                if (preExistResult.Value)
                {
                    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(new GetBlobsOptions() { Traits = BlobTraits.Metadata }))
                    {
                        if (blobItem.Metadata != null && blobItem.Metadata.TryGetValue("PathWithName", out string nameValue))
                        {
                            if (!string.IsNullOrEmpty(nameValue) && nameValue == filepath)
                            {
                                return blobItem.Metadata;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fail to get stored files from Blog.");
            }

            return null;
        }
    }
}
