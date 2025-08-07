using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsRadioPartDisplayDriver : ContentPartDisplayDriver<GdsRadioPart>
    {
        public override IDisplayResult Edit(GdsRadioPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsRadioPartViewModel>("GdsRadioPart_Fields_Edit", model =>
            {
                model.Value = part.Value;
                model.LabelClass = part.LabelClass;
                model.Label = part.Label;
                model.Selected = part.Selected;
                model.ConditionalFlow = part.ConditionalFlow;

                model.ContentItem = part.ContentItem;
                model.GdsRadioPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsRadioPart part, IUpdateModel updater)
        {
            var viewModel = new GdsRadioPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Value = viewModel.Value?.Trim();
                part.LabelClass = viewModel.LabelClass?.Trim();
                part.Label = viewModel.Label?.Trim();
                part.Selected = viewModel.Selected;
                part.ConditionalFlow = viewModel.ConditionalFlow;
            }

            return Edit(part);
        }
    }
}
