using Microsoft.AspNetCore.Http;

namespace OrchardCore.GDS.Session.Helpers
{
    public class SessionKeyHelper 
    {
        public readonly ISession? _session;
        private readonly string _sessionId;

        public SessionKeyHelper(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor?.HttpContext?.Session;
            _sessionId = _session?.Id ?? string.Empty;
     
            if (_session != null && !_session.Keys.Any())
            {
                _session.Set("SessionCreated", SessionCreatedBytes);
            }
        }

        public string GetSessionKey(string key) => $"session:{_sessionId}:{key}";

        private static readonly byte[] SessionCreatedBytes = new byte[] { 49 };
    }
}
