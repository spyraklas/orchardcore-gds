using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using OrchardCore.Shared.Session.Config;
using OrchardCore.Shared.Session.Handlers;

namespace OrchardCore.Shared.Session
{
    public sealed class Startup : StartupBase
    {
        private readonly IShellConfiguration _shellConfiguration;
        private readonly IConfiguration _configuration;

        public Startup(IShellConfiguration shellConfiguration, IConfiguration configuration)
        {
            _shellConfiguration = shellConfiguration;
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // Bind session settings from tenant (shell) if present, otherwise from global configuration
            services.Configure<SessionSettings>(options =>
            {
                var tenantSection = _shellConfiguration.GetSection("SessionSettings");
                var globalSection = _configuration.GetSection("SessionSettings");

                if (!string.IsNullOrEmpty(_shellConfiguration.GetValue<string>("SessionSettings:Provider")) ||
                    !string.IsNullOrEmpty(_shellConfiguration.GetValue<string>("SessionSettings:RedisConnectionString")))
                {
                    tenantSection.Bind(options);
                }
                else
                {
                    globalSection.Bind(options);
                }
            });

            // Determine provider and register appropriate distributed cache and session handler
            var tenantProvider = _shellConfiguration.GetValue<string>("SessionSettings:Provider");
            var globalProvider = _configuration["SessionSettings:Provider"];
            var provider = !string.IsNullOrEmpty(tenantProvider) ? tenantProvider : globalProvider ?? "Memory";

            if (provider.Equals("Redis", StringComparison.OrdinalIgnoreCase))
            {
                var tenantRedis = _shellConfiguration.GetValue<string>("SessionSettings:RedisConnectionString");
                var globalRedis = _configuration["SessionSettings:RedisConnectionString"];
                var redisConn = !string.IsNullOrEmpty(tenantRedis) ? tenantRedis : globalRedis;

                if (!string.IsNullOrEmpty(redisConn))
                {
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = redisConn;
                        options.InstanceName = "session:";
                    });

                    services.AddScoped<ISessionHandler, RedisSessionHandler>();
                }
                else
                {
                    // Fallback to memory cache if no connection string provided
                    services.AddDistributedMemoryCache();
                    services.AddScoped<ISessionHandler, MemoryCacheSessionHandler>();
                }
            }
            else
            {
                services.AddDistributedMemoryCache();
                services.AddScoped<ISessionHandler, MemoryCacheSessionHandler>();
            }
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "OrchardCore.Shared.Session",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
            base.Configure(builder, routes, serviceProvider);
        }
    }

}
