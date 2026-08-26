using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OrchardCore.Shared.Session.Config;
using StackExchange.Redis;

namespace OrchardCore.Shared.Session.Handlers
{
    internal class RedisSessionHandler : ISessionHandler
    {
        private readonly IDatabase _db;
        private readonly SessionSettings _settings;

        public RedisSessionHandler(IConnectionMultiplexer multiplexer, IOptions<SessionSettings> settings)
        {
            _db = multiplexer.GetDatabase();
            _settings = settings.Value;
        }

        private string Key(string sessionId, string key) => $"session:{sessionId}:{key}";

        public Task SetAsync<T>(string sessionId, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            var expiry = TimeSpan.FromMinutes(Math.Max(1, _settings.ExpirationMinutes));
            return _db.StringSetAsync(Key(sessionId, key), json, expiry);
        }

        public async Task<T?> GetAsync<T>(string sessionId, string key)
        {
            var value = await _db.StringGetAsync(Key(sessionId, key));

            if (value.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T?>(value.ToString());
        }

        public Task RemoveAsync(string sessionId, string key)
        {
            return _db.KeyDeleteAsync(Key(sessionId, key));
        }

        public Task ClearAsync(string sessionId)
        {
            // Redis doesn't support listing keys via IDatabase without server access. Not implemented here.
            return Task.CompletedTask;
        }
    }
}
