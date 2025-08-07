using System.ComponentModel.DataAnnotations;

namespace OrchardCore.GDS.Components.Workflows.ViewModels
{
    public class ValidateNumberOfFilesTaskViewModel
    {
        [Required]
        public string FieldName { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        public int MaxNumberOfFiles { get; set; }

        [Required]
        public string SessionKey { get; set; }
    }
}
