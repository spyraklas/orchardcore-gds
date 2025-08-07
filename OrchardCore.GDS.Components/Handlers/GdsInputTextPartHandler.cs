using OrchardCore.ContentManagement.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Handlers
{
    public class GdsInputTextPartHandler : ContentPartHandler<GdsInputTextPart>
    {
        public override Task InitializingAsync(InitializingContentContext context, GdsInputTextPart part)
        {
            part.Value = string.Empty;
            part.Placeholder = string.Empty;
            part.Prefix = string.Empty;
            part.Suffix = string.Empty;
            part.AutoComplete = string.Empty;
            part.Type = "text";
            part.SpellCheck = false;
            part.SessionKey = string.Empty;

            return Task.CompletedTask;
        }
    }
}