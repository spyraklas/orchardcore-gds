using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.DSI.Models;
using OrchardCore.DSI.ViewModels;

namespace OrchardCore.DSI.Drivers
{
    public class DSIPagePartDisplayDriver : ContentPartDisplayDriver<DSIPagePart>
    {
        public override IDisplayResult Display(DSIPagePart part, BuildPartDisplayContext context)
        {
            return Initialize<DSIPageViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(DSIPagePart part, BuildPartEditorContext context)
        {
            return Initialize<DSIPageViewModel>(GetEditorShapeType(context), model =>
            {
                model.InvalidRedirectUrl = part.InvalidRedirectUrl;
                model.ValidatedRoles = part.ValidatedRoles;

                model.ContentItem = part.ContentItem;
                model.UserValidatorPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(DSIPagePart part, UpdatePartEditorContext context)
        {
            var viewModel = new DSIPageViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.InvalidRedirectUrl = viewModel.InvalidRedirectUrl?.Trim();
                part.ValidatedRoles = viewModel.ValidatedRoles?.Trim();
            }

            return await EditAsync(part, context);
        }

        private Task BuildViewModel(DSIPageViewModel model, DSIPagePart part, BuildPartDisplayContext context)
        {
            model.ContentItem = part.ContentItem;
            model.InvalidRedirectUrl = part.InvalidRedirectUrl;
            model.ValidatedRoles = part.ValidatedRoles;
            model.UserValidatorPart = part;

            return Task.CompletedTask;
        }
    }
}

