using System.ComponentModel.DataAnnotations;

namespace OrchardCore.GDS.Components.Workflows.ViewModels
{
    public class ValidateWordCountTaskViewModel
    {
        [Required]
        public string FieldName { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
