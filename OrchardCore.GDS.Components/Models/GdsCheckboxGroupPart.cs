using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsCheckboxGroupPart : ContentPart
    {
        public bool SmallCheckboxes { get; set; }
        public string SessionKey { get; set; }
        public bool Autosave { get; set; }
    }
}
