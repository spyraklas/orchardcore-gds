using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsAccordionSectionPartViewModel
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }
        public string TitleStyle { get; set; }
        public string TitleClass { get; set; }
        public string Summary { get; set; }
        public string SummaryClass { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsAccordionSectionPart GdsAccordionSectionPart { get; set; }
    }
}
