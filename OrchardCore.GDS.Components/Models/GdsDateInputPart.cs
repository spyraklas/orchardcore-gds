using OrchardCore.ContentManagement;

namespace OrchardCore.GDS.Components.Models
{
    public class GdsDateInputPart : ContentPart
    {
        public string DayValue { get; set; }
        public string MonthValue { get; set; }
        public string YearValue { get; set; }
        public string AutoComplete { get; set; }
        public string DaySessionKey { get; set; }
        public string MonthSessionKey { get; set; }
        public string YearSessionKey { get; set; }
    }
}
