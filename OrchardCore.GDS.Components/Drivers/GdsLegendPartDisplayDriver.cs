using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsLegendPartDisplayDriver : ContentPartDisplayDriver<GdsLegendPart>
    {
        public override IDisplayResult Edit(GdsLegendPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsLegendPartViewModel>("GdsLegendPart_Fields_Edit", model =>
            {
                model.LegendSize = part.LegendSize;

                model.ContentItem = part.ContentItem;
                model.GdsLegendPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsLegendPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsLegendPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.LegendSize = viewModel.LegendSize?.Trim();
            }

            return await EditAsync(part, context);
        }
    }
}
