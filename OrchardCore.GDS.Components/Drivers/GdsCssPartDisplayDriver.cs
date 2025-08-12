using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsCssPartDisplayDriver : ContentPartDisplayDriver<GdsCssPart>
    {
        public override IDisplayResult Edit(GdsCssPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsCssPartViewModel>("GdsCssPart_Fields_Edit", model =>
            {
                model.CssClass = part.CssClass;
                model.CssStyle = part.CssStyle;

                model.ContentItem = part.ContentItem;
                model.GdsCssPart = part;
            })
            .Location("Parts:0#CSS;40");
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsCssPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsCssPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.CssClass = viewModel.CssClass?.Trim();
                part.CssStyle = viewModel.CssStyle?.Trim();
            }

            return await EditAsync(part, context);
        }
    }
}
