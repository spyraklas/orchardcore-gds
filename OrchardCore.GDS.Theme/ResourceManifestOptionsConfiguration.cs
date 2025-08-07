using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.GDS.Theme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineScript("gds-theme")
                .SetUrl("~/OrchardCore.GDS.Theme/site.min.js", "~/OrchardCore.GDS.Theme/site.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("gds-theme")
                .SetUrl("~/OrchardCore.GDS.Theme/site.min.css", "~/OrchardCore.GDS.Theme/site.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("gds-theme-frontend")
                .SetUrl("~/OrchardCore.GDS.Theme/govuk-frontend.min.js", "~/OrchardCore.GDS.Theme/govuk-frontend.min.js")
                .SetVersion("5.11.0");

            _manifest
                .DefineStyle("gds-theme-frontend")
                .SetUrl("~/OrchardCore.GDS.Theme/all.min.css", "~/OrchardCore.GDS.Theme/all.css")
                .SetVersion("5.11.0");

            _manifest
                .DefineScript("gds-theme-jquery")
                .SetUrl("~/OrchardCore.GDS.Theme/jquery.min.js", "~/OrchardCore.GDS.Theme/jquery.js")
                .SetVersion("3.7.1");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
