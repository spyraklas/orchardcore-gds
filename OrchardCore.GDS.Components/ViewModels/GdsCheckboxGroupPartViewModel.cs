using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.GDS.Components.Models;
using System.Runtime.Serialization;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsCheckboxGroupPartViewModel
    {
        public string LegendSize { get; set; }
        public string LegendStyle { get; set; }
        public bool SmallCheckboxes { get; set; }
        public string SessionKey { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsCheckboxGroupPart GdsCheckboxGroupPart { get; set; }

        [IgnoreDataMember]
        [BindNever]
        public BuildPartDisplayContext BuildPartDisplayContext { get; set; }
    }
}
