using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsSelectOptionPartDisplayDriver : ContentPartDisplayDriver<GdsSelectOptionPart>
    {
        public override IDisplayResult Edit(GdsSelectOptionPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsSelectOptionPartViewModel>("GdsSelectOptionPart_Fields_Edit", model =>
            {
                model.Label = part.Label;
                model.Value = part.Value;
                model.Selected = part.Selected;

                model.ContentItem = part.ContentItem;
                model.GdsSelectOptionPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsSelectOptionPart part, IUpdateModel updater)
        {
            var viewModel = new GdsSelectOptionPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Value = viewModel.Value?.Trim();
                part.Label = viewModel.Label?.Trim();
                part.Selected = viewModel.Selected;
            }

            return Edit(part);
        }
    }
}
