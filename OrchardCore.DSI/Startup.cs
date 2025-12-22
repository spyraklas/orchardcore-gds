using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.DSI.Config;
using OrchardCore.DSI.Core;
using OrchardCore.DSI.Core.Extensions;
using OrchardCore.DSI.Drivers;
using OrchardCore.DSI.Handlers;
using OrchardCore.DSI.Models;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace OrchardCore.DSI
{
    [RequireFeatures("OrchardCore.Title", "OrchardCore.Autoroute", "OrchardCore.Flows")]
    public class Startup : StartupBase
    {
        private readonly IConfiguration _configuration;
        private readonly IShellConfiguration _shellConfiguration;
        private readonly ShellSettings _shellSettings;
        public readonly ILogger<Startup> _logger;

        public Startup(IShellConfiguration shellConfiguration, IConfiguration configuration, ShellSettings shellSettings, ILogger<Startup> logger)
        {
            _shellConfiguration = shellConfiguration;
            _configuration = configuration;
            _shellSettings = shellSettings;
            _logger = logger;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            ConfigureSettings(services);

            ConfigureDI(services);

            ConfigureDSI(services);

            ConfigureOrchardParts(services);

            ConfigureDataMigration(services);
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            routes.MapAreaControllerRoute(
                name: "DSI",
                areaName: "OrchardCore.DSI",
                pattern: "DSI/Login",
                defaults: new { controller = "DSI", action = "Login" }
            );
            routes.MapAreaControllerRoute(
                name: "DSI",
                areaName: "OrchardCore.DSI",
                pattern: "DSI/Logout",
                defaults: new { controller = "DSI", action = "Logout" }
            );
        }

        #region private methods
        private void ConfigureOrchardParts(IServiceCollection services)
        {
            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<DSIPagePart>();
            });

            //DSI Page
            services.AddContentPart<DSIPagePart>()
                .UseDisplayDriver<DSIPagePartDisplayDriver>()
                .AddHandler<DSIPagePartHandler>();
        }

        private void ConfigureDataMigration(IServiceCollection services)
        {
            services.AddDataMigration<Migrations>();
        }

        private void ConfigureDSI(IServiceCollection services)
        {
            var dfeSignInConfig = new DfESignInConfig();

            _shellConfiguration.GetSection(key: nameof(DfESignInConfig)).Bind(dfeSignInConfig);
            if (!dfeSignInConfig.UseDfeSignin)
            {
                _configuration.GetSection(key: nameof(DfESignInConfig)).Bind(dfeSignInConfig);
                if (!dfeSignInConfig.UseDfeSignin)
                {
                    _logger.LogInformation("DfE Sign-In is disabled or no configuration exist.");
                    return;
                }
            }

            services.AddDfESignInAuthentication(dfeSignInConfig, _shellSettings);

            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.FromDays(120);
            });
            services.AddRouting();
            services.AddHttpContextAccessor();
        }
        
        private void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<IDSIUserHandler, DSIUserHandler>();
        }

        private void ConfigureSettings(IServiceCollection services)
        {
            string tenantClientId = _shellConfiguration.GetValue<string>("DfESignInConfig:ClientId");
            string globalClientId = _configuration["DfESignInConfig:ClientId"];

            if (!string.IsNullOrEmpty(tenantClientId))
            {
                services.Configure<DfESignInConfig>(options => _shellConfiguration.GetSection("DfESignInConfig").Bind(options));
            }
            else if (!string.IsNullOrEmpty(globalClientId))
            {
                services.Configure<DfESignInConfig>(options => _configuration.GetSection("DfESignInConfig").Bind(options));
            }
            else
            {
                _logger.LogInformation("DfE Sign-In configuration section is missing.");
                throw new System.Exception("DfE Sign-In configuration section is missing.");
            }
        }

        #endregion
    }

}
