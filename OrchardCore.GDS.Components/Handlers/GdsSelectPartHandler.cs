using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsSelectPartHandler : ContentPartHandler<GdsSelectPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsSelectPart part)
        {
            part.SessionKey = string.Empty;

            return Task.CompletedTask;
        }
    }
}