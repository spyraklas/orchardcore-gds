using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsUploadFilesPartDisplayDriver : ContentPartDisplayDriver<GdsUploadFilesPart>
    {
        public override IDisplayResult Display(GdsUploadFilesPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsUploadFilesPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsUploadFilesPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsUploadFilesPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.UploadAction = part.UploadAction;
                model.ButtonValue = part.ButtonValue;
                model.FileTypesAllowed = part.FileTypesAllowed;
                model.FilesSessionKey = part.FilesSessionKey;
                model.WithDownload = part.WithDownload;
                model.DownloadAction = part.DownloadAction;
                model.WithRemove = part.WithRemove;
                model.RemoveAction = part.RemoveAction;

                model.ContentItem = part.ContentItem;
                model.GdsUploadFilesPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsUploadFilesPart part, IUpdateModel updater)
        {
            var viewModel = new GdsUploadFilesPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.UploadAction = viewModel.UploadAction?.Trim();
                part.ButtonValue = viewModel.ButtonValue?.Trim();
                part.FileTypesAllowed = viewModel.FileTypesAllowed?.Trim();
                part.FilesSessionKey = viewModel.FilesSessionKey?.Trim();
                part.DownloadAction = viewModel.DownloadAction?.Trim();
                part.RemoveAction = viewModel.RemoveAction?.Trim();
                part.WithDownload = viewModel.WithDownload;
                part.WithRemove = viewModel.WithRemove;
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsUploadFilesPartViewModel model, GdsUploadFilesPart part)
        {
            model.ContentItem = part.ContentItem;

            model.UploadAction = part.UploadAction;
            model.ButtonValue = part.ButtonValue;
            model.FileTypesAllowed = part.FileTypesAllowed;
            model.FilesSessionKey = part.FilesSessionKey;
            model.WithDownload = part.WithDownload;
            model.DownloadAction = part.DownloadAction;
            model.WithRemove = part.WithRemove;
            model.RemoveAction = part.RemoveAction;

            model.GdsUploadFilesPart = part;

            return Task.CompletedTask;
        }
    }
}
