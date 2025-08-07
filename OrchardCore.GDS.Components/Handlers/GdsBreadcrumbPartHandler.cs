using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsBreadcrumbPartHandler : ContentPartHandler<GdsBreadcrumbPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsBreadcrumbPart part)
        {
            part.Href = string.Empty;
            part.Value = string.Empty;

            return Task.CompletedTask;
        }
    }
}