using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OrchardCore.Shared.Session.Config;

namespace OrchardCore.Shared.Session.Handlers
{
    public class MemoryCacheSessionHandler : ISessionHandler
    {
        private readonly IMemoryCache _cache;
        private readonly SessionSettings _settings;

        public MemoryCacheSessionHandler(IMemoryCache cache, IOptions<SessionSettings> settings)
        {
            _cache = cache;
            _settings = settings.Value;
        }

        private string Key(string sessionId, string key) => $"session:{sessionId}:{key}";

        public Task SetAsync<T>(string sessionId, string key, T value)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Math.Max(1, _settings.ExpirationMinutes))
            };

            _cache.Set(Key(sessionId, key), JsonSerializer.Serialize(value), options);
            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string sessionId, string key)
        {
            if (_cache.TryGetValue(Key(sessionId, key), out string? json))
            {
                return Task.FromResult(JsonSerializer.Deserialize<T?>(json));
            }

            return Task.FromResult<T?>(default);
        }

        public Task RemoveAsync(string sessionId, string key)
        {
            _cache.Remove(Key(sessionId, key));
            return Task.CompletedTask;
        }

        public Task ClearAsync(string sessionId)
        {
            // IMemoryCache doesn't support enumerating keys; nothing to do here in a simple implementation
            return Task.CompletedTask;
        }
    }
}
