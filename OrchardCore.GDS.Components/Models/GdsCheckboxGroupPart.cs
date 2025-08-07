using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsCheckboxGroupPart : ContentPart
    {
        public string LegendSize { get; set; }
        public string LegendStyle { get; set; }
        public bool SmallCheckboxes { get; set; }
        public string SessionKey { get; set; }
    }
}
