using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OrchardCore.Workflows.Options;

namespace OrchardCore.GDS.Session.Helpers;

public class SessionHelper : ISessionHelper
{
    private readonly IHttpContextAccessor? _httpContextAccessor;
    public readonly IOptions<WorkflowOptions> _workflowOptions;

    public SessionHelper(IHttpContextAccessor httpContextAccessor, IOptions<WorkflowOptions> workflowOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _workflowOptions = workflowOptions;
    }

    public string GetSessionValue(string key)
    {
        var httpSession = _httpContextAccessor?.HttpContext?.Session;
        if (string.IsNullOrEmpty(key) || httpSession == null)
        {
            return string.Empty;
        }

        var value = httpSession.GetString(key);
        return value ?? string.Empty;
    }

    public void SetSessionValue(string key, string value)
    {
        var httpSession = _httpContextAccessor?.HttpContext?.Session;
        if (!string.IsNullOrEmpty(key))
        {
            if (value == null)
            {
                httpSession?.Remove(key);
            }
            else
            {
                httpSession?.SetString(key, value);
            }
        }
    }
}