using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class BlobFilesEditPartViewModel
    {
        public string FileReference { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public BlobFilesEditPart BlobFilesEditPart { get; set; }

        [BindNever]
        public BlobFilesEditSettings Settings { get; set; }
    }
}
