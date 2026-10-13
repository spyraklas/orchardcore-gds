using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using OrchardCore.GDS.Session.Helpers;

namespace OrchardCore.GDS.Session.MemoryCache.Helpers;

//public class Session
//{
//}

//public class SessionHelper : SessionKeyHelper, ISessionHelper 
//{
//    private readonly IMemoryCache _memoryCache;
//    private readonly string _sessionId;

//    public string SessionId { get; }

//    public SessionHelper(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor): 
//        base(httpContextAccessor)
//    {
//        _memoryCache = memoryCache;
//        var session = httpContextAccessor?.HttpContext?.Session;
//        _sessionId = session?.Id ?? string.Empty;
//        SessionId = session?.Id ?? string.Empty;

//        //if (session != null && !session.Keys.Any())
//        //{
//        //    session.Set("SessionCreated", SessionCreatedBytes);
//        //}
//    }

//    // private string SessionKey(string key) => $"session:{_sessionId}:{key}";

//    public string GetSessionValue(string key)
//    {
//        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(_sessionId))
//        {
//            return string.Empty;
//        }

//        var sessionKey = GetSessionKey(key);
//        _memoryCache.TryGetValue(sessionKey, out string? result);

//        return result ?? string.Empty;
//    }

//    public void SetSessionValue(string key, string value)
//    {
//        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(_sessionId))
//        {
//            return;
//        }

//        var sessionKey = GetSessionKey(key);
//        _memoryCache.Set(sessionKey, value);
//    }

//    private static readonly byte[] SessionCreatedBytes = new byte[] { 49 };
//}