using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsCheckboxGroupPartHandler : ContentPartHandler<GdsCheckboxGroupPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsCheckboxGroupPart part)
        {
            part.LegendStyle = "label";
            part.SmallCheckboxes = false;
            part.SessionKey = string.Empty;

            return Task.CompletedTask;
        }
    }
}