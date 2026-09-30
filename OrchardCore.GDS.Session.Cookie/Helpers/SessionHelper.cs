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
        var session = _httpContextAccessor?.HttpContext?.Session;
        if (session == null || string.IsNullOrEmpty(sessionKey))
        {
            return string.Empty;
        }

        var sessionValue = session.GetString(sessionKey);
        return sessionValue ?? string.Empty;
    }

    public void SetSessionValue(string key, string value)
    {
        var session = _httpContextAccessor?.HttpContext?.Session;
        if (session != null)
        {
            session.SetString(key, value);
        }
    }
}