using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.DSI.Models;

namespace OrchardCore.DSI.ViewModels
{
    public class DSIPageViewModel
    {
        public string ValidatedRoles { get; set; }
        public string InvalidRedirectUrl { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public DSIPagePart UserValidatorPart { get; set; }
    }
}
