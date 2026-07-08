using OrchardCore.ContentManagement;

namespace OrchardCore.DSI.Models
{
    public class DSIPagePart : ContentPart
    {
        public string ValidatedRoles { get; set; }

        public string InvalidRedirectUrl { get; set; }
    }
}
