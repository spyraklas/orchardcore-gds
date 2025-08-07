namespace OrchardCore.GDS.Components.Models
{
    public class BlobFile
    {
        public string PathWithName { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public long Size { get; set; }
    }
}
