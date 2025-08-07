using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsCheckboxPartHandler : ContentPartHandler<GdsCheckboxPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsCheckboxPart part)
        {
            part.Value = string.Empty;
            part.Label = string.Empty;
            part.LabelClass = string.Empty;
            part.Selected = false;
            part.ConditionalFlow = false;

            return Task.CompletedTask;
        }
    }
}