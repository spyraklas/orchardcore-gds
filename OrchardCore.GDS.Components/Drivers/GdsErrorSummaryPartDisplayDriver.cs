using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsErrorSummaryPartDisplayDriver : ContentPartDisplayDriver<GdsErrorSummaryPart>
    {
        public override IDisplayResult Display(GdsErrorSummaryPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsErrorSummaryPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsErrorSummaryPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsErrorSummaryPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Title = part.Title;

                model.ContentItem = part.ContentItem;
                model.GdsErrorSummaryPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsErrorSummaryPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsErrorSummaryPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Title = viewModel.Title?.Trim();
            }

            return await EditAsync(part, context);
        }

        private Task BuildViewModel(GdsErrorSummaryPartViewModel model, GdsErrorSummaryPart part, BuildPartDisplayContext context)
        {
            model.ContentItem = part.ContentItem;
            model.Title = part.Title;

            return Task.CompletedTask;
        }
    }
}
