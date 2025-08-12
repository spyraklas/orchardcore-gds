using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsLabelPartDisplayDriver : ContentPartDisplayDriver<GdsLabelPart>
    {
        public override IDisplayResult Edit(GdsLabelPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsLabelPartViewModel>("GdsLabelPart_Fields_Edit", model =>
            {
                model.Label = part.Label;
                model.LabelClass = part.LabelClass;
                model.LabelStyle = part.LabelStyle;
                model.LabelSize = part.LabelSize;

                model.ContentItem = part.ContentItem;
                model.GdsLabelPart = part;
            })
            .Location("Parts:0#Label;19");
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsLabelPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsLabelPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Label = viewModel.Label?.Trim();
                part.LabelClass = viewModel.LabelClass?.Trim();
                part.LabelStyle = viewModel.LabelStyle?.Trim();
                part.LabelSize = viewModel.LabelSize?.Trim();
            }

            return await EditAsync(part, context);
        }
    }
}
