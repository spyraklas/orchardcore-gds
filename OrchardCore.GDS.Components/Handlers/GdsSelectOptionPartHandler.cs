using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsSelectOptionPartHandler : ContentPartHandler<GdsSelectOptionPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsSelectOptionPart part)
        {
            part.Value = string.Empty;
            part.Label = string.Empty;
            part.Selected = false;

            return Task.CompletedTask;
        }
    }
}