using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsTextareaPart : ContentPart
    {
        public string Value { get; set; }
        public int Rows { get; set; }
        public bool SpellCheck { get; set; }
        public bool CountWords { get; set; }
        public int Count { get; set; }
        public string SessionKey { get; set; }
    }
}
