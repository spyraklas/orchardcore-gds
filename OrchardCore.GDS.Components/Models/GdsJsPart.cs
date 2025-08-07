using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsJsPart : ContentPart
    {
        public string OnChange { get; set; }
        public string OnClick { get; set; }
        public string OnMouseOver { get; set; }
        public string OnMouseOut { get; set; }
        public string OnKeyDown { get; set; }
        public string OnLoad { get; set; }
    }
}
