using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsLabelPartViewModel
    {
        public string Label { get; set; }
        public string LabelStyle { get; set; }
        public string LabelSize { get; set; }
        public string LabelClass { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsLabelPart GdsLabelPart { get; set; }
    }
}
