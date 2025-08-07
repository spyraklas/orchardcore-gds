using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsUploadFilesPartHandler : ContentPartHandler<GdsUploadFilesPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsUploadFilesPart part)
        {
            part.UploadAction = string.Empty;
            part.ButtonValue = string.Empty;
            part.FileTypesAllowed = string.Empty;
            part.FilesSessionKey = string.Empty;
            part.DownloadAction = string.Empty;
            part.RemoveAction = string.Empty;
            part.WithDownload = false;
            part.WithRemove = false;

            return Task.CompletedTask;
        }
    }
}