using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsJsPartDisplayDriver : ContentPartDisplayDriver<GdsJsPart>
    {
        public override IDisplayResult Edit(GdsJsPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsJsPartViewModel>("GdsJsPart_Fields_Edit", model =>
            {
                model.OnChange = part.OnChange;
                model.OnClick = part.OnClick;
                model.OnMouseOver = part.OnMouseOver;
                model.OnMouseOut = part.OnMouseOut;
                model.OnKeyDown = part.OnKeyDown;
                model.OnLoad = part.OnLoad;

                model.ContentItem = part.ContentItem;
                model.GdsJsPart = part;
            })
            .Location("Parts:0#Javascript;41");
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsJsPart part, IUpdateModel updater)
        {
            var viewModel = new GdsJsPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.OnChange = viewModel.OnChange?.Trim();
                part.OnClick = viewModel.OnClick?.Trim();
                part.OnMouseOver = viewModel.OnMouseOver?.Trim();
                part.OnMouseOut = viewModel.OnMouseOut?.Trim();
                part.OnKeyDown = viewModel.OnKeyDown?.Trim();
                part.OnLoad = viewModel.OnLoad?.Trim();
            }

            return Edit(part);
        }
    }
}
