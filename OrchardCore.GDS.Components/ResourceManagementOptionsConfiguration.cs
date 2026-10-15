using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.GDS.Components
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;
        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineScript("gds-file-manager")
                .SetUrl("~/OrchardCore.GDS.Components/js/gds-file-manager.min.js", "~/OrchardCore.GDS.Components/js/gds-file-manager.js")
                .SetDependencies("jQuery")
                .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }

    }
}
