using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class BlobFilesEditSettingsViewModel
    {
        public string FileTypesAllowed { get; set; }
        public int FileSizeAllowed { get; set; }
        public bool WithDownload { get; set; }
        public bool WithRemove { get; set; }

        [BindNever]
        public BlobFilesEditSettings BlobFilesEditSettings { get; set; }
    }
}
