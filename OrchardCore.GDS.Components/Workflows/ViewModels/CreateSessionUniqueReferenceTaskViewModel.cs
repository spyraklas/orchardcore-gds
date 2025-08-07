using System.ComponentModel.DataAnnotations;

namespace OrchardCore.GDS.Components.Workflows.ViewModels
{
    public class CreateSessionUniqueReferenceTaskViewModel
    {
        [Required]
        public string SessionKey { get; set; }
    }
}
