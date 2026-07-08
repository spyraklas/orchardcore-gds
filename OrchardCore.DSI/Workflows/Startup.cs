using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DSI.Workflows.Activities;
using OrchardCore.DSI.Workflows.Drivers;
using OrchardCore.Modules;
using OrchardCore.Workflows.Helpers;

namespace OrchardCore.DSI.Workflows
{
    [RequireFeatures("OrchardCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<ValidateGdsUserTask, ValidateGdsUserTaskDisplayDriver>();
        }
    }
}
