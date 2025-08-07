using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsButtonPart : ContentPart
    {
        public string Type { get; set; }
        public string Style { get; set; }
        public string Value { get; set; }
        public string Class { get; set; }
        public string OnClick { get; set; }
        public string Href { get; set; }
        public string Target { get; set; }
        public bool Disabled { get; set; }
        public bool PreventDoubleClick { get; set; }
    }
}
