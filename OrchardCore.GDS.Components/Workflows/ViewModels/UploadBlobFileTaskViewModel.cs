using System.ComponentModel.DataAnnotations;

namespace OrchardCore.GDS.Components.Workflows.ViewModels
{
    public class UploadBlobFileTaskViewModel
    {
        public string FilePath { get; set; }

        [Required]
        public string SessionKey { get; set; }
    }
}
