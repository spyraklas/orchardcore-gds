using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.ViewModels
{
    public class GdsInputTextPartViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string AutoComplete { get; set; }
        public string Placeholder { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool SpellCheck { get; set; }
        public string SessionKey { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GdsInputTextPart GdsTextInputPart { get; set; }
    }
}
