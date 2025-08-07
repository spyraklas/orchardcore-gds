using Microsoft.AspNetCore.Http;
using System.Text;

namespace OrchardCore.GDS.Components.Helpers
{
    public class SessionHelper : ISessionHelper
    {
        public string GetSessionValue(string sessionKey, IHttpContextAccessor httpContextAccessor)
        {
            string result = string.Empty;

            if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Session != null && !string.IsNullOrEmpty(sessionKey))
            {
                string sessionValue = httpContextAccessor.HttpContext.Session.GetString(sessionKey);
                result = sessionValue != null ? sessionValue : "";
            }

            return result;
        }
    }
}
