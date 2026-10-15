using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Settings
{
    public class BlobFilesEditSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver<BlobFilesEditPart>
    {
        internal readonly IStringLocalizer S;

        public BlobFilesEditSettingsDisplayDriver(IStringLocalizer<BlobFilesEditSettingsDisplayDriver> localizer)
        {
            S = localizer;
        }

        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, BuildEditorContext context)
        {
            return Initialize<BlobFilesEditSettingsViewModel>("BlobFilesEditSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<BlobFilesEditSettings>();

                model.FileTypesAllowed = settings.FileTypesAllowed;
                model.FileSizeAllowed = settings.FileSizeAllowed;
                model.WithDownload = settings.WithDownload;
                model.WithRemove = settings.WithRemove;

                model.BlobFilesEditSettings = settings;

            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            var model = new BlobFilesEditSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix,
                m => m.FileTypesAllowed,
                m => m.FileSizeAllowed,
                m => m.WithDownload,
                m => m.WithRemove);

            context.Builder.WithSettings(new BlobFilesEditSettings
            {
                FileTypesAllowed = model.FileTypesAllowed,
                FileSizeAllowed = model.FileSizeAllowed,
                WithDownload = model.WithDownload,
                WithRemove = model.WithRemove
            });

            return Edit(contentTypePartDefinition, context);
        }
    }
}
