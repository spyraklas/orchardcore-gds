using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsRadioPartViewModel
    {
        public string Value { get; set; }
        public string Label { get; set; }
        public string LabelClass { get; set; }
        public bool Selected { get; set; }
        public bool ConditionalFlow { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsRadioPart GdsRadioPart { get; set; }
    }
}
