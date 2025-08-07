using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsCheckboxDividerPartHandler : ContentPartHandler<GdsCheckboxDividerPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsCheckboxDividerPart part)
        {
            part.Value = string.Empty;

            return Task.CompletedTask;
        }
    }
}