using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class BlobFilesEditPartDisplayDriver : ContentPartDisplayDriver<BlobFilesEditPart>
    {
        public override IDisplayResult Display(BlobFilesEditPart part, BuildPartDisplayContext context)
        {
            return Initialize<BlobFilesEditPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(BlobFilesEditPart part, BuildPartEditorContext context)
        {
            return Initialize<BlobFilesEditPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.FileReference = part.FileReference;

                model.ContentItem = part.ContentItem;
                model.BlobFilesEditPart = part;
                model.Settings = context.TypePartDefinition.GetSettings<BlobFilesEditSettings>();
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(BlobFilesEditPart part, UpdatePartEditorContext context)
        {
            var viewModel = new BlobFilesEditPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.FileReference = viewModel.FileReference;
            }

            return await EditAsync(part, context);
        }

        private Task BuildViewModel(BlobFilesEditPartViewModel model, BlobFilesEditPart part, BuildPartDisplayContext context)
        {
            model.ContentItem = part.ContentItem;

            model.FileReference = part.FileReference;

            model.BlobFilesEditPart = part;
            model.Settings = context.TypePartDefinition.GetSettings<BlobFilesEditSettings>();

            return Task.CompletedTask;
        }
    }
}
