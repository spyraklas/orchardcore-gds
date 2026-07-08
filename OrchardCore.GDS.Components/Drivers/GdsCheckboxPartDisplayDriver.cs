using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsCheckboxPartDisplayDriver : ContentPartDisplayDriver<GdsCheckboxPart>
    {
        public override IDisplayResult Edit(GdsCheckboxPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsCheckboxPartViewModel>("GdsCheckboxPart_Fields_Edit", model =>
            {
                model.Value = part.Value;
                model.LabelClass = part.LabelClass;
                model.Label = part.Label;
                model.Selected = part.Selected;
                model.ConditionalFlow = part.ConditionalFlow;

                model.ContentItem = part.ContentItem;
                model.GdsCheckboxPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsCheckboxPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsCheckboxPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Value = viewModel.Value?.Trim();
                part.LabelClass = viewModel.LabelClass?.Trim();
                part.Label = viewModel.Label?.Trim();
                part.Selected = viewModel.Selected;
                part.ConditionalFlow = viewModel.ConditionalFlow;
            }

            return await EditAsync(part, context);
        }
    }
}
