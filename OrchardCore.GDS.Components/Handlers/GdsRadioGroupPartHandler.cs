using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsRadioGroupPartHandler : ContentPartHandler<GdsRadioGroupPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsRadioGroupPart part)
        {
            part.LegendStyle = "label";
            part.Inline = false;
            part.SmallRadios = false;
            part.SessionKey = string.Empty;

            return Task.CompletedTask;
        }
    }
}