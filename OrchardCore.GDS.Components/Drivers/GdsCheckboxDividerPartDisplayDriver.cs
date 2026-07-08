using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsCheckboxDividerPartDisplayDriver : ContentPartDisplayDriver<GdsCheckboxDividerPart>
    {
        public override IDisplayResult Edit(GdsCheckboxDividerPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsCheckboxDividerPartViewModel>("GdsCheckboxDividerPart_Fields_Edit", model =>
            {
                model.Value = part.Value;

                model.ContentItem = part.ContentItem;
                model.GdsCheckboxDividerPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsCheckboxDividerPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsCheckboxDividerPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Value = viewModel.Value?.Trim();
            }

            return await EditAsync(part, context);
        }
    }
}
