using OrchardCore.ContentManagement.Handlers;
using OrchardCore.DSI.Models;

namespace OrchardCore.DSI.Handlers
{

    public class DSIPagePartHandler : ContentPartHandler<DSIPagePart>
    {
        public override Task InitializingAsync(InitializingContentContext context, DSIPagePart part)
        {
            part.InvalidRedirectUrl = string.Empty;
            part.ValidatedRoles = string.Empty;
            return Task.CompletedTask;
        }
    }
}
