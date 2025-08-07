using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsButtonPartHandler : ContentPartHandler<GdsButtonPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsButtonPart part)
        {
            part.Type = "button";
            part.Style = string.Empty;
            part.Disabled = false;
            part.PreventDoubleClick = false;

            return Task.CompletedTask;
        }
    }
}