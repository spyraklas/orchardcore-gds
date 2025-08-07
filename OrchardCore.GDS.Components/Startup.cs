using Azure.Storage.Blobs;
using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.GDS.Components.Config;
using OrchardCore.GDS.Components.Drivers;
using OrchardCore.GDS.Components.Handlers;
using OrchardCore.GDS.Components.Helpers;
using OrchardCore.GDS.Components.Liquid;
using OrchardCore.GDS.Components.Models;
using OrchardCore.Liquid;
using OrchardCore.Modules;

namespace OrchardCore.GDS.Components
{
    [RequireFeatures("OrchardCore.Forms", "OrchardCore.Flows")]
    public class Startup : StartupBase
    {
        private readonly IShellConfiguration _shellConfiguration;
        private readonly IConfiguration _configuration;
        public readonly ILogger<Startup> _logger;

        public Startup(IShellConfiguration shellConfiguration, IConfiguration configuration, ILogger<Startup> logger)
        {
            _shellConfiguration = shellConfiguration;
            _configuration = configuration;
            _logger = logger;
        }

        #region Public Functions
        public override void ConfigureServices(IServiceCollection services)
        {
            ConfigureSession(services);

            ConfigureSettings(services);

            ConfigureLiquid(services);

            ConfigureDI(services);

            ConfigureOrchardParts(services);

            ConfigureDataMigration(services);
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.UseSession();

            base.Configure(app, routes, serviceProvider);
        }
        #endregion

        #region Private Functions
        private void ConfigureSession(IServiceCollection services)
        {
            
            int tenantSessionTimeout = _shellConfiguration.GetValue<int>("SessionSettings:Timeout");
            int globalSessionTimeout = Convert.ToInt32(_configuration["SessionSettings:Timeout"]);

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                if (tenantSessionTimeout > 0)
                    options.IdleTimeout = TimeSpan.FromMinutes(tenantSessionTimeout);
                else if(globalSessionTimeout > 0)
                    options.IdleTimeout = TimeSpan.FromMinutes(globalSessionTimeout);
                else
                    options.IdleTimeout = TimeSpan.FromMinutes(30);

                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        private void ConfigureSettings(IServiceCollection services)
        {
            services.Configure<AntivirusSettings>(options => _configuration.GetSection("AntivirusSettings").Bind(options));

            string tenantBlobConnection = _shellConfiguration.GetValue<string>("BlobSettings:ConnectionString");
            if (!string.IsNullOrEmpty(tenantBlobConnection))
                services.Configure<BlobSettings>(options => _shellConfiguration.GetSection("BlobSettings").Bind(options));
            else
                services.Configure<BlobSettings>(options => _configuration.GetSection("BlobSettings").Bind(options));
        }

        private void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<ISessionHelper, SessionHelper>();
            services.AddScoped<IBlobFilesHelper, BlobFilesHelper>();
            services.AddScoped<IBlobFilesHandler, BlobFilesHandler>();

            string tenantBlobConnection = _shellConfiguration.GetValue<string>("BlobSettings:ConnectionString"); 
            string globalBlobConnection = _configuration["BlobSettings:ConnectionString"];

            if (!string.IsNullOrEmpty(tenantBlobConnection))
                services.AddScoped(instance => new BlobServiceClient(tenantBlobConnection));
            else if(!string.IsNullOrEmpty(globalBlobConnection))
                services.AddScoped(instance => new BlobServiceClient(globalBlobConnection));
            else
            {
                _logger.LogWarning(@"Fail to find configuration for blob connection (""BlobSettings:ConnectionString"")");
                services.AddScoped(instance => new BlobServiceClient("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;"));
            }
        }

        private void ConfigureOrchardParts(IServiceCollection services)
        {
            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<GdsCssPart>();
                o.MemberAccessStrategy.Register<GdsJsPart>();
                o.MemberAccessStrategy.Register<GdsLabelPart>();
                o.MemberAccessStrategy.Register<GdsHintPart>();
                o.MemberAccessStrategy.Register<GdsLegendPart>();
                o.MemberAccessStrategy.Register<GdsInputTextPart>();
                o.MemberAccessStrategy.Register<GdsTextareaPart>();
                o.MemberAccessStrategy.Register<GdsErrorSummaryPart>();
                o.MemberAccessStrategy.Register<GdsButtonPart>();
                o.MemberAccessStrategy.Register<GdsBreadcrumbPart>();
                o.MemberAccessStrategy.Register<GdsBreadcrumbsPart>();
                o.MemberAccessStrategy.Register<GdsCheckboxPart>();
                o.MemberAccessStrategy.Register<GdsCheckboxDividerPart>();
                o.MemberAccessStrategy.Register<GdsCheckboxGroupPart>();
                o.MemberAccessStrategy.Register<GdsRadioPart>();
                o.MemberAccessStrategy.Register<GdsRadioDividerPart>();
                o.MemberAccessStrategy.Register<GdsRadioGroupPart>();
                o.MemberAccessStrategy.Register<GdsAccordionPart>();
                o.MemberAccessStrategy.Register<GdsAccordionSectionPart>();
                o.MemberAccessStrategy.Register<GdsSelectPart>();
                o.MemberAccessStrategy.Register<GdsSelectOptionPart>();
                o.MemberAccessStrategy.Register<GdsDateInputPart>();
                o.MemberAccessStrategy.Register<GdsUploadFilesPart>();
            });

            //Gds Css Attributes
            services.AddContentPart<GdsCssPart>()
                .UseDisplayDriver<GdsCssPartDisplayDriver>()
                .AddHandler<GdsCssPartHandler>();

            //Gds Js Attributes
            services.AddContentPart<GdsJsPart>()
                .UseDisplayDriver<GdsJsPartDisplayDriver>()
                .AddHandler<GdsJsPartHandler>();

            //Gds Label
            services.AddContentPart<GdsLabelPart>()
                .UseDisplayDriver<GdsLabelPartDisplayDriver>()
                .AddHandler<GdsLabelPartHandler>();

            //Gds Hint
            services.AddContentPart<GdsHintPart>()
                .UseDisplayDriver<GdsHintPartDisplayDriver>()
                .AddHandler<GdsHintPartHandler>();

            //Gds Legend
            services.AddContentPart<GdsLegendPart>()
                .UseDisplayDriver<GdsLegendPartDisplayDriver>()
                .AddHandler<GdsLegendPartHandler>();

            //Gds Input Text
            services.AddContentPart<GdsInputTextPart>()
                .UseDisplayDriver<GdsInputTextPartDisplayDriver>()
                .AddHandler<GdsInputTextPartHandler>();

            //Gds Textarea
            services.AddContentPart<GdsTextareaPart>()
                .UseDisplayDriver<GdsTextareaPartDisplayDriver>()
                .AddHandler<GdsTextareaPartHandler>();

            //Gds Error Summary
            services.AddContentPart<GdsErrorSummaryPart>()
                .UseDisplayDriver<GdsErrorSummaryPartDisplayDriver>()
                .AddHandler<GdsErrorSummaryPartHandler>();

            //Gds Button
            services.AddContentPart<GdsButtonPart>()
                .UseDisplayDriver<GdsButtonPartDisplayDriver>()
                .AddHandler<GdsButtonPartHandler>();

            //Gds Breadcrumb
            services.AddContentPart<GdsBreadcrumbPart>()
                .UseDisplayDriver<GdsBreadcrumbPartDisplayDriver>()
                .AddHandler<GdsBreadcrumbPartHandler>();

            //Gds Breadcrumbs
            services.AddContentPart<GdsBreadcrumbsPart>()
                .UseDisplayDriver<GdsBreadcrumbsPartDisplayDriver>()
                .AddHandler<GdsBreadcrumbsPartHandler>();

            //Gds Checkbox
            services.AddContentPart<GdsCheckboxPart>()
                .UseDisplayDriver<GdsCheckboxPartDisplayDriver>()
                .AddHandler<GdsCheckboxPartHandler>();

            //Gds Checkbox Divider
            services.AddContentPart<GdsCheckboxDividerPart>()
                .UseDisplayDriver<GdsCheckboxDividerPartDisplayDriver>()
                .AddHandler<GdsCheckboxDividerPartHandler>();

            //Gds Checkbox Group
            services.AddContentPart<GdsCheckboxGroupPart>()
                .UseDisplayDriver<GdsCheckboxGroupPartDisplayDriver>()
                .AddHandler<GdsCheckboxGroupPartHandler>();

            //Gds Radio
            services.AddContentPart<GdsRadioPart>()
                .UseDisplayDriver<GdsRadioPartDisplayDriver>()
                .AddHandler<GdsRadioPartHandler>();

            //Gds Radio Divider
            services.AddContentPart<GdsRadioDividerPart>()
                .UseDisplayDriver<GdsRadioDividerPartDisplayDriver>()
                .AddHandler<GdsRadioDividerPartHandler>();

            //Gds Radio Group
            services.AddContentPart<GdsRadioGroupPart>()
                .UseDisplayDriver<GdsRadioGroupPartDisplayDriver>()
                .AddHandler<GdsRadioGroupPartHandler>();

            //Gds Accordion
            services.AddContentPart<GdsAccordionPart>()
                .UseDisplayDriver<GdsAccordionPartDisplayDriver>()
                .AddHandler<GdsAccordionPartHandler>();

            //Gds Accordion Section
            services.AddContentPart<GdsAccordionSectionPart>()
                .UseDisplayDriver<GdsAccordionSectionPartDisplayDriver>()
                .AddHandler<GdsAccordionSectionPartHandler>();

            //Gds Select
            services.AddContentPart<GdsSelectPart>()
                .UseDisplayDriver<GdsSelectPartDisplayDriver>()
                .AddHandler<GdsSelectPartHandler>();

            //Gds Select Option
            services.AddContentPart<GdsSelectOptionPart>()
                .UseDisplayDriver<GdsSelectOptionPartDisplayDriver>()
                .AddHandler<GdsSelectOptionPartHandler>();

            //Gds Date Input
            services.AddContentPart<GdsDateInputPart>()
                .UseDisplayDriver<GdsDateInputPartDisplayDriver>()
                .AddHandler<GdsDateInputPartHandler>();

            //Gds Upload Files
            services.AddContentPart<GdsUploadFilesPart>()
                .UseDisplayDriver<GdsUploadFilesPartDisplayDriver>()
                .AddHandler<GdsUploadFilesPartHandler>();

        }

        private void ConfigureLiquid(IServiceCollection services)
        {
            services.AddLiquidFilter<SessionFilter>("session");
        }

        private void ConfigureDataMigration(IServiceCollection services)
        {
            services.AddDataMigration<Migrations>();
        }
        #endregion
    }
}
