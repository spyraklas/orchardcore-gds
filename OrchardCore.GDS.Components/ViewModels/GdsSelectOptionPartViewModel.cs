using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsSelectOptionPartViewModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsSelectOptionPart GdsSelectOptionPart { get; set; }
    }
}
