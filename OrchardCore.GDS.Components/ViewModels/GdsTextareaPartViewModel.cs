using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsTextareaPartViewModel
    {
        public string Value { get; set; }
        public int Rows { get; set; }
        public bool SpellCheck { get; set; }
        public bool CountWords { get; set; }
        public int Count { get; set; }
        public string SessionKey { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsTextareaPart GdsTextareaPart { get; set; }
    }
}
