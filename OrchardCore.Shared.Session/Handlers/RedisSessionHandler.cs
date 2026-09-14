using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using OrchardCore.Shared.Session.Config;

namespace OrchardCore.Shared.Session.Handlers
{
    internal class RedisSessionHandler : ISessionHandler
    {
        private readonly IDistributedCache _cache;
        private readonly SessionSettings _settings;

        public RedisSessionHandler(IDistributedCache cache, IOptions<SessionSettings> settings)
        {
            _cache = cache;
            _settings = settings.Value;
        }

        private string Key(string sessionId, string key) => $"session:{sessionId}:{key}";

        public Task SetAsync<T>(string sessionId, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            var expiry = TimeSpan.FromMinutes(Math.Max(1, _settings.ExpirationMinutes));

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry
            };

            return _cache.SetStringAsync(Key(sessionId, key), json, options);
        }

        public async Task<T?> GetAsync<T>(string sessionId, string key)
        {
            var value = await _cache.GetStringAsync(Key(sessionId, key));

            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T?>(value);
        }

        public Task RemoveAsync(string sessionId, string key)
        {
            return _cache.RemoveAsync(Key(sessionId, key));
        }

        public Task ClearAsync(string sessionId)
        {
            // Redis doesn't support listing keys via IDatabase without server access. Not implemented here.
            return Task.CompletedTask;
        }
    }
}
