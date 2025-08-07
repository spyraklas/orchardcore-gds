using System.ComponentModel.DataAnnotations;

namespace OrchardCore.GDS.Components.Workflows.ViewModels
{
    public class ValidateRegexFormFieldTaskViewModel
    {
        [Required]
        public string FieldName { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        public string Regex { get; set; }
    }
}
