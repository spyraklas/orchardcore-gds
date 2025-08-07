using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsJsPartHandler : ContentPartHandler<GdsJsPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsJsPart part)
        {
            part.OnChange = string.Empty;
            part.OnClick = string.Empty;
            part.OnMouseOver = string.Empty;
            part.OnMouseOut = string.Empty;
            part.OnKeyDown = string.Empty;
            part.OnLoad = string.Empty;

            return Task.CompletedTask;
        }
    }
}