using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsAccordionPartHandler : ContentPartHandler<GdsAccordionPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsAccordionPart part)
        {
            part.Id = string.Empty;
            part.Class = string.Empty;

            return Task.CompletedTask;
        }
    }
}