using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsHintPartDisplayDriver : ContentPartDisplayDriver<GdsHintPart>
    {
        public override IDisplayResult Edit(GdsHintPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsHintPartViewModel>("GdsHintPart_Fields_Edit", model =>
            {
                model.Hint = part.Hint;
                model.HintClass = part.HintClass;
 
                model.ContentItem = part.ContentItem;
                model.GdsHintPart = part;
            })
            .Location("Parts:0#Hint;20");

        }

        public override async Task<IDisplayResult> UpdateAsync(GdsHintPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsHintPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Hint = viewModel.Hint?.Trim();
                part.HintClass = viewModel.HintClass?.Trim();
            }

            return await EditAsync(part, context);
        }
    }
}
