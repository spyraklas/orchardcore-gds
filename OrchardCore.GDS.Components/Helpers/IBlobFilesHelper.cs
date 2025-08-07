using Microsoft.AspNetCore.Http;

namespace OrchardCore.GDS.Components.Handlers
{
    public interface IBlobFilesHelper
    {
        Task UploadFile(IFormFile file, string fileReference);
    }
}
