using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsAccordionSectionPart : ContentPart
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }
        public string TitleStyle { get; set; }
        public string TitleClass { get; set; }
        public string Summary { get; set; }
        public string SummaryClass { get; set; }
    }
}
