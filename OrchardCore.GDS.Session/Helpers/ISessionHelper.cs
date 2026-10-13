using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;

namespace OrchardCore.GDS.Session.Helpers;

public interface ISessionHelper
{
    string GetSessionValue(string sessionKey);

    void SetSessionValue(string key, string value);

    void Save();

    IHtmlContent SessionKeyField(string fieldName, string sessionKey);
}
