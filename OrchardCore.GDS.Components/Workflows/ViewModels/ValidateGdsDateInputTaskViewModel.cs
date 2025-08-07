using System.ComponentModel.DataAnnotations;

namespace OrchardCore.GDS.Components.Workflows.ViewModels
{
    public class ValidateGdsDateInputTaskViewModel
    {
        [Required]
        public string FieldName { get; set; }

        [Required]
        public string FieldTitle { get; set; }

        [Required]
        public string ErrorMessage { get; set; }
    }
}
