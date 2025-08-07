using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsHintPartHandler : ContentPartHandler<GdsHintPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsHintPart part)
        {
            part.Hint = string.Empty;
            part.HintClass = string.Empty;

            return Task.CompletedTask;
        }
    }
}