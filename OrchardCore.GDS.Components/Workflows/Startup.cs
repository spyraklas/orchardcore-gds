using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Workflows.Helpers;
using OrchardCore.Modules;
using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.Drivers;

namespace OrchardCore.GDS.Components.Workflows
{
    [RequireFeatures("OrchardCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<ValidateWordCountTask, ValidateWordCountTaskDisplayDriver>();
            services.AddActivity<ValidateCharacterCountTask, ValidateCharacterCountTaskDisplayDriver>();
            services.AddActivity<ValidateRegexFormFieldTask, ValidateRegexFormFieldTaskDisplayDriver>();
            services.AddActivity<ValidateGdsDateInputTask, ValidateGdsDateInputTaskDisplayDriver>();
            services.AddActivity<CreateSessionVariablesTask, CreateSessionVariablesTaskDisplayDriver>();
            services.AddActivity<CreateSessionUniqueReferenceTask, CreateSessionUniqueReferenceTaskDisplayDriver>();
            services.AddActivity<UploadBlobFileTask, UploadBlobFileTaskDisplayDriver>();
            services.AddActivity<DownloadBlobFileTask, DownloadBlobFileTaskDisplayDriver>();
            services.AddActivity<RemoveBlobFileTask, RemoveBlobFileTaskDisplayDriver>();
            services.AddActivity<ValidateNumberOfFilesTask, ValidateNumberOfFilesTaskDisplayDriver>();
        }
    }
}
