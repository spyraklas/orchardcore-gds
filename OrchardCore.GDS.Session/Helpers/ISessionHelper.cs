namespace OrchardCore.GDS.Session.Helpers;

public interface ISessionHelper
{
    string GetSessionValue(string sessionKey);

    void SetSessionValue(string key, string value);
}
