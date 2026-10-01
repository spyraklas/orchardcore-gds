using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;
using OrchardCore.Mvc.ModelBinding;

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

                model.ButtonValue = settings.ButtonValue;
                model.UploadAction = settings.UploadAction;
                model.FileTypesAllowed = settings.FileTypesAllowed;
                model.WithDownload = settings.WithDownload;
                model.DownloadAction = settings.DownloadAction;
                model.WithRemove = settings.WithRemove;
                model.RemoveAction = settings.RemoveAction;

                model.BlobFilesEditSettings = settings;

            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            var model = new BlobFilesEditSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix,
                m => m.ButtonValue,
                m => m.UploadAction,
                m => m.FileTypesAllowed,
                m => m.WithDownload,
                m => m.DownloadAction,
                m => m.WithRemove,
                m => m.RemoveAction);

            if (string.IsNullOrWhiteSpace(model.UploadAction))
            {
                context.Updater.ModelState.AddModelError(Prefix, nameof(model.UploadAction), S["The upload action is required."]);
            }

            if (string.IsNullOrWhiteSpace(model.ButtonValue))
            {
                context.Updater.ModelState.AddModelError(Prefix, nameof(model.ButtonValue), S["The button value is required."]);
            }

            if (model.WithDownload && string.IsNullOrWhiteSpace(model.DownloadAction))
            {
                context.Updater.ModelState.AddModelError(Prefix, nameof(model.DownloadAction), S["The download action is required when the download option is enabled."]);
            }

            if (model.WithRemove && string.IsNullOrWhiteSpace(model.RemoveAction))
            {
                context.Updater.ModelState.AddModelError(Prefix, nameof(model.RemoveAction), S["The remove action is required when the remove option is enabled."]);
            }

            context.Builder.WithSettings(new BlobFilesEditSettings
            {
                ButtonValue = model.ButtonValue,
                UploadAction = model.UploadAction,
                FileTypesAllowed = model.FileTypesAllowed,
                WithDownload = model.WithDownload,
                DownloadAction = model.DownloadAction,
                WithRemove = model.WithRemove,
                RemoveAction = model.RemoveAction
            });

            return Edit(contentTypePartDefinition, context);
        }
    }
}
