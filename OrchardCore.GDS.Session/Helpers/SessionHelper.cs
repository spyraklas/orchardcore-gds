using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OrchardCore.Workflows.Options;

namespace OrchardCore.GDS.Session.Helpers;

public class SessionHelper : ISessionHelper
{
    private const string GDSComponentSessionKey = "__gdsComponentSessionKey";   
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

    public void Save()
    {
        var form = _httpContextAccessor?.HttpContext?.Request?.Form;
        if (form == null || !form.ContainsKey(GDSComponentSessionKey))
        {
            return;
        }

        var sessionKeys = form[GDSComponentSessionKey]
            .Select(v => v.Split(':'))
            .ToDictionary(
                parts => parts[0],
                parts => parts[1]);

        foreach (var item in sessionKeys)
        {
            var sessionKey = item.Value;
            var value = form[item.Key];
            SetSessionValue(sessionKey, value);
        }
    }

    public IHtmlContent SessionKeyField(string fieldName, string sessionKey)
    {
        var html = $"<input name=\"{GDSComponentSessionKey}\" type=\"hidden\" value='{fieldName}:{sessionKey}'>";
        return new HtmlString(html);
    }
}