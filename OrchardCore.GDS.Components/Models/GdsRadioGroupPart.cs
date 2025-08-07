using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsRadioGroupPart : ContentPart
    {
        public string LegendSize { get; set; }
        public string LegendStyle { get; set; }
        public bool Inline { get; set; }
        public bool SmallRadios { get; set; }
        public string SessionKey { get; set; }

    }
}
