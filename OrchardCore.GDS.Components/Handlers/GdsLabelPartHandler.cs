using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsLabelPartHandler : ContentPartHandler<GdsLabelPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsLabelPart part)
        {
            part.Label = string.Empty;
            part.LabelClass = string.Empty;
            part.LabelStyle = "label";
            part.LabelSize = "govuk-label--m";

            return Task.CompletedTask;
        }
    }
}