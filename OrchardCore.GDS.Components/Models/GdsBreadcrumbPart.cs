using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsBreadcrumbPart  : ContentPart
    {
        public string Value { get; set; }
        public string Href { get; set; }
    }
}
