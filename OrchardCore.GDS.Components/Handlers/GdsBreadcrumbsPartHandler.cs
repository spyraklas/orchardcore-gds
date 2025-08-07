using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsBreadcrumbsPartHandler : ContentPartHandler<GdsBreadcrumbsPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsBreadcrumbsPart part)
        {
            part.MobileColapse = false;

            return Task.CompletedTask;
        }
    }
}