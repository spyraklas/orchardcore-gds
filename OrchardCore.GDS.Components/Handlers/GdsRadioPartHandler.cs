using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsRadioPartHandler : ContentPartHandler<GdsRadioPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsRadioPart part)
        {
            part.Value = string.Empty;
            part.ConditionalFlow = false;
            part.Label = string.Empty;
            part.LabelClass = string.Empty;
            part.Selected = false;
            part.ConditionalFlow = false;

            return Task.CompletedTask;
        }
    }
}