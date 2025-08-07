using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsCssPartHandler : ContentPartHandler<GdsCssPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsCssPart part)
        {
            part.CssClass = string.Empty;
            part.CssStyle = string.Empty;

            return Task.CompletedTask;
        }
    }
}