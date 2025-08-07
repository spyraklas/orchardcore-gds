using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsTextareaPartHandler : ContentPartHandler<GdsTextareaPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsTextareaPart part)
        {
            part.Value = string.Empty;
            part.SpellCheck = false;
            part.Rows = 5;
            part.Count = 0;
            part.CountWords = false;
            part.SessionKey = string.Empty;

            return Task.CompletedTask;
        }
    }
}