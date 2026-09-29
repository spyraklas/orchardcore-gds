using Microsoft.AspNetCore.Http;
using OrchardCore.GDS.Session.Helpers;
using System.Text;

namespace OrchardCore.GDS.Session.Cookie.Helpers;

public class SessionHelper : ISessionHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetSessionValue(string sessionKey)
    {
        string result = string.Empty;

        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null && !string.IsNullOrEmpty(sessionKey))
        {
            string sessionValue = _httpContextAccessor.HttpContext.Session.GetString(sessionKey);
            result = sessionValue != null ? sessionValue : "";
        }

        return result;
    }
}