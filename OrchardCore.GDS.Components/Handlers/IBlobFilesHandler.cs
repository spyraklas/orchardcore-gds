using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public interface IBlobFilesHandler
    {
        Task<BlobUploadResult> UploadAsync(BlobFile document, Dictionary<string, string> metaDataDictionary);
        Task<List<BlobFile>> GetStoredFilesAsync(string fileReference);
        Task<BlobFile> DownloadAsync(string fileWithPath);
        Task<bool> DeleteAsync(string fileWithPath);
    }
}