using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsRadioDividerPartHandler : ContentPartHandler<GdsRadioDividerPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsRadioDividerPart part)
        {
            part.Value = string.Empty;

            return Task.CompletedTask;
        }
    }
}