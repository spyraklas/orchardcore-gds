namespace OrchardCore.GDS.Components.Models
{
    public class BlobFilesEditSettings
    {
        public string FileTypesAllowed { get; set; }
        public int FileSizeAllowed   { get; set; }
        public bool WithDownload { get; set; }
        public bool WithRemove { get; set; }
    }
}
