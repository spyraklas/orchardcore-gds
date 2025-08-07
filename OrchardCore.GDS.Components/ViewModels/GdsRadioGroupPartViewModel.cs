using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.GDS.Components.Models;
using System.Runtime.Serialization;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsRadioGroupPartViewModel
    {
        public string LegendSize { get; set; }
        public string LegendStyle { get; set; }
        public bool Inline { get; set; }
        public bool SmallRadios { get; set; }
        public string SessionKey { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsRadioGroupPart GdsRadioGroupPart { get; set; }

        [IgnoreDataMember]
        [BindNever]
        public BuildPartDisplayContext BuildPartDisplayContext { get; set; }
    }
}
