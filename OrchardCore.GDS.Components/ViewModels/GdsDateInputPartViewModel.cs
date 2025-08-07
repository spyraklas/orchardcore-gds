using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsDateInputPartViewModel
    {
        public string DayValue { get; set; }
        public string MonthValue { get; set; }
        public string YearValue { get; set; }
        public string AutoComplete { get; set; }
        public string DaySessionKey { get; set; }
        public string MonthSessionKey { get; set; }
        public string YearSessionKey { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsDateInputPart GdsDateInputPart { get; set; }
    }
}
