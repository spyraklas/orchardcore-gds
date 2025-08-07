using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.GDS.Components.Models;
using System.Runtime.Serialization;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsAccordionPartViewModel
    {
        public string Id { get; set; }
        public string Class { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsAccordionPart GdsAccordionPart { get; set; }

        [IgnoreDataMember]
        [BindNever]
        public BuildPartDisplayContext BuildPartDisplayContext { get; set; }
    }
}
