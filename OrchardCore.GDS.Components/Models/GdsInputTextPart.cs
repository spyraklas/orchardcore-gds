using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsInputTextPart : ContentPart
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Placeholder { get; set; }
        public string AutoComplete { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool SpellCheck { get; set; }
        public string SessionKey { get; set; }
    }
}
