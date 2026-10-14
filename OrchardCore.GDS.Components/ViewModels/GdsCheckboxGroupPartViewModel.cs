using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.GDS.Components.Models;
using System.Runtime.Serialization;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsCheckboxGroupPartViewModel
    {
        public bool SmallCheckboxes { get; set; }
        public string SessionKey { get; set; }
        public bool Autosave { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsCheckboxGroupPart GdsCheckboxGroupPart { get; set; }

        [IgnoreDataMember]
        [BindNever]
        public BuildPartDisplayContext BuildPartDisplayContext { get; set; }
    }
}
