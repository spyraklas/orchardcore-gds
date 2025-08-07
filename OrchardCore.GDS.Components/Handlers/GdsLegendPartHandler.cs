using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsLegendPartHandler : ContentPartHandler<GdsLegendPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsLegendPart part)
        {
            part.LegendSize = string.Empty;

            return Task.CompletedTask;
        }
    }
}