using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using System.ComponentModel;

namespace OrchardCore.DSI
{
    public sealed class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IRecipeMigrator _recipeMigrator;
        private readonly ILogger<Migrations> _logger;

        public Migrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator, ILogger<Migrations> logger)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
            _logger = logger;
        }

        public async Task<int> CreateAsync()
        {
            //UserValidatorPart
            await _contentDefinitionManager.AlterPartDefinitionAsync("DSIPagePart", builder => builder
                .WithDescription("Provides a DSI User Validator for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("DSIPage", type => type
                .WithPart("TitlePart", part => part
                    .WithSettings(new TitlePartSettings
                    {
                        RenderTitle = false,
                    })
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "{{ Model.ContentItem | display_text | slugify }}",
                        AllowRouteContainedItems = true,
                    })
                    .WithPosition("3")
                )
                .WithPart("DSIPagePart", part => part
                    .WithPosition("5")
                )
                .WithPart("FlowPart", part => part
                    .WithPosition("7")
                )
                .Securable()
                .Versionable()
                .Creatable()
                .Draftable()
                .Listable());

            return 1;
        }
    }
    internal sealed class TitlePartSettings
    {
        public int Options { get; set; }

        public string Pattern { get; set; }

        [DefaultValue(true)]
        public bool RenderTitle { get; set; }
    }

    internal sealed class AutoroutePartSettings
    {
        public string Pattern { get; set; }
        public bool AllowRouteContainedItems { get; set; }
    }
}