using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class BlobFilesEditPartHandler : ContentPartHandler<BlobFilesEditPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, BlobFilesEditPart part)
        {
            part.FileReference = string.Empty;

            return Task.CompletedTask;
        }
    }
}