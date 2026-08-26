using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.GDS.NeetTheme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineScript("gds-theme")
                .SetUrl("~/OrchardCore.GDS.NeetTheme/js/site.min.js", "~/OrchardCore.GDS.NeetTheme/js/site.js")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("gds-theme")
                .SetUrl("~/OrchardCore.GDS.NeetTheme/css/site.min.css", "~/OrchardCore.GDS.NeetTheme/css/site.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineStyle("gds-theme-neet")
                .SetUrl("~/OrchardCore.GDS.NeetTheme/css/neet.min.css", "~/OrchardCore.GDS.NeetTheme/css/neet.css")
                .SetVersion("1.0.0");

            _manifest
                .DefineScript("gds-theme-frontend")
                .SetUrl("~/OrchardCore.GDS.NeetTheme/js/govuk-frontend.min.js", "~/OrchardCore.GDS.NeetTheme/js/govuk-frontend.min.js")
                .SetVersion("6.3.0");

            _manifest
                .DefineStyle("gds-theme-frontend")
                .SetUrl("~/OrchardCore.GDS.NeetTheme/css/govuk-frontend.min.css", "~/OrchardCore.GDS.NeetTheme/css/govuk-frontend.css")
                .SetVersion("6.3.0");

            _manifest
                .DefineScript("gds-theme-jquery")
                .SetUrl("~/OrchardCore.GDS.NeetTheme/js/jquery.min.js", "~/OrchardCore.GDS.NeetTheme/js/jquery.js")
                .SetVersion("4.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
