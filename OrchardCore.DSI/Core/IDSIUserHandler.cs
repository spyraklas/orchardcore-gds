using OrchardCore.DSI.Core.Models;

namespace OrchardCore.DSI.Core
{
    public interface IDSIUserHandler
    {
        Task<bool> IsAuthorized();

        Task<bool> IsAuthenticated(string roles);

        Task<User> GetLoginUser();
    }
}
