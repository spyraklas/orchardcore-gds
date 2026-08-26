using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Shared.Session.Config;
using System.Collections.Generic;

namespace OrchardCore.Shared.Session.Services
{
    internal class SessionExpirationCleanupService : BackgroundService
    {
        private readonly IEnumerable<Handlers.IExpirableSessionStore> _stores;
        private readonly ILogger<SessionExpirationCleanupService> _logger;
        private readonly SessionSettings _settings;

        public SessionExpirationCleanupService(IEnumerable<Handlers.IExpirableSessionStore> stores, IOptions<SessionSettings> settings, ILogger<SessionExpirationCleanupService> logger)
        {
            _stores = stores;
            _logger = logger;
            _settings = settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var interval = TimeSpan.FromMinutes(Math.Max(1, _settings.ExpirationMinutes));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach (var store in _stores)
                    {
                        try
                        {
                            await store.CleanExpiredAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Error cleaning expired sessions for store {Store}", store.GetType().Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during session cleanup loop");
                }

                await Task.Delay(interval, stoppingToken);
            }
        }
    }
}
