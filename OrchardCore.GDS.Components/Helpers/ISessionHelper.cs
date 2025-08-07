using Microsoft.AspNetCore.Http;

namespace OrchardCore.GDS.Components.Helpers
{
    public interface ISessionHelper
    {
        string GetSessionValue(string sessionKey, IHttpContextAccessor httpContextAccessor);
    }
}
