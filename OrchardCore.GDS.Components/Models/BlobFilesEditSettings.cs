namespace OrchardCore.GDS.Components.Models
{
    public class BlobFilesEditSettings
    {
        public string UploadAction { get; set; }
        public string ButtonValue { get; set; }
        public string FileTypesAllowed { get; set; }
        public bool WithDownload { get; set; }
        public string DownloadAction { get; set; }
        public bool WithRemove { get; set; }
        public string RemoveAction { get; set; }
    }
}
