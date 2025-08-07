using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsAccordionSectionPartHandler : ContentPartHandler<GdsAccordionSectionPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsAccordionSectionPart part)
        {
            part.Id = string.Empty;
            part.Class = string.Empty;
            part.Title = string.Empty;
            part.TitleStyle = "label";
            part.TitleClass = string.Empty;
            part.Summary = string.Empty;
            part.SummaryClass = string.Empty;

            return Task.CompletedTask;
        }
    }
}