using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsBreadcrumbPartViewModel
    {
        public string Value { get; set; }
        public string Href { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsBreadcrumbPart GdsBreadcrumbPart { get; set; }
    }
}
