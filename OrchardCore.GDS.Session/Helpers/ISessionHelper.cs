using System;
using System.Collections.Generic;
using System.Text;

namespace OrchardCore.GDS.Session.Helpers
{
    public interface ISessionHelper
    {
       // public string SessionId { get; }

        string GetSessionValue(string sessionKey);

        void SetSessionValue(string key, string value);
    }
}
