using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsSelectOptionPart : ContentPart
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}
