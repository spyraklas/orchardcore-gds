using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsRadioDividerPartDisplayDriver : ContentPartDisplayDriver<GdsRadioDividerPart>
    {
        public override IDisplayResult Edit(GdsRadioDividerPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsRadioDividerPartViewModel>("GdsRadioDividerPart_Fields_Edit", model =>
            {
                model.Value = part.Value;

                model.ContentItem = part.ContentItem;
                model.GdsRadioDividerPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsRadioDividerPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsRadioDividerPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Value = viewModel.Value?.Trim();
            }

            return await EditAsync(part, context);
        }
    }
}
