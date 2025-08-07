using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsErrorSummaryPartHandler : ContentPartHandler<GdsErrorSummaryPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsErrorSummaryPart part)
        {
            part.Title = "There is a problem";

            return Task.CompletedTask;
        }
    }
}