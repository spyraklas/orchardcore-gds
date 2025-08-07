using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.GDS.Components.Models;
using System.Runtime.Serialization;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsBreadcrumbsPartViewModel
    {
        public bool MobileColapse { get; set; }

        [IgnoreDataMember]
        [BindNever]
        public BuildPartDisplayContext BuildPartDisplayContext { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsBreadcrumbsPart GdsBreadcrumbsPart { get; set; }
    }
}
