using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsRadioGroupPart : ContentPart
    {
        public bool Inline { get; set; }
        public bool SmallRadios { get; set; }
        public string SessionKey { get; set; }

    }
}
