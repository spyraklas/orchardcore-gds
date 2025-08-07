using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsUploadFilesPartViewModel
    {
        public string UploadAction { get; set; }
        public string ButtonValue { get; set; }
        public string FileTypesAllowed { get; set; }
        public string FilesSessionKey { get; set; }
        public bool WithDownload { get; set; }
        public string DownloadAction { get; set; }
        public bool WithRemove { get; set; }
        public string RemoveAction { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsUploadFilesPart GdsUploadFilesPart { get; set; }
    }
}
