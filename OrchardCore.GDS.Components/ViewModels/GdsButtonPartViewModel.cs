using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsButtonPartViewModel
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

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsButtonPart GdsButtonPart { get; set; }
    }
}
