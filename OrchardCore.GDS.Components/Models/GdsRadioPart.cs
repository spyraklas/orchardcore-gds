using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsRadioPart : ContentPart
    {
        public string Value { get; set; }
        public string Label { get; set; }
        public string LabelClass { get; set; }
        public bool OnClick { get; set; }
        public bool Selected { get; set; }
        public bool ConditionalFlow { get; set; }
    }
}
