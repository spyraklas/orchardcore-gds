using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsJsPartViewModel
    {
        public string OnChange { get; set; }
        public string OnClick { get; set; }
        public string OnMouseOver { get; set; }
        public string OnMouseOut { get; set; }
        public string OnKeyDown { get; set; }
        public string OnLoad { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsJsPart GdsJsPart { get; set; }
    }
}
