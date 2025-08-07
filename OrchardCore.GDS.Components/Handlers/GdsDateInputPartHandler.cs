using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsDateInputPartHandler : ContentPartHandler<GdsDateInputPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsDateInputPart part)
        {
            part.DayValue = string.Empty;
            part.MonthValue = string.Empty;
            part.YearValue = string.Empty;
            part.AutoComplete = string.Empty;
            part.DaySessionKey = string.Empty;
            part.MonthSessionKey = string.Empty;
            part.YearSessionKey = string.Empty;

            return Task.CompletedTask;
        }
    }
}