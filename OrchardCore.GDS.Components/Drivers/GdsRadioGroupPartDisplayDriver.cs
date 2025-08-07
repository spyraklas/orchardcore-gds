using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsRadioGroupPartDisplayDriver : ContentPartDisplayDriver<GdsRadioGroupPart>
    {
        public override IDisplayResult Display(GdsRadioGroupPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsRadioGroupPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsRadioGroupPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsRadioGroupPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.LegendSize = part.LegendSize;
                model.LegendStyle = part.LegendStyle;
                model.Inline = part.Inline;
                model.SmallRadios = part.SmallRadios;
                model.SessionKey = part.SessionKey;

                model.ContentItem = part.ContentItem;
                model.GdsRadioGroupPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsRadioGroupPart part, IUpdateModel updater)
        {
            var viewModel = new GdsRadioGroupPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.LegendSize = viewModel.LegendSize?.Trim();
                part.LegendStyle = viewModel.LegendStyle?.Trim();
                part.Inline = viewModel.Inline;
                part.SmallRadios = viewModel.SmallRadios;
                part.SessionKey = viewModel.SessionKey?.Trim();
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsRadioGroupPartViewModel model, GdsRadioGroupPart part, BuildPartDisplayContext context)
        {
            model.ContentItem = part.ContentItem;

            model.LegendSize = part.LegendSize;
            model.LegendStyle = part.LegendStyle;
            model.Inline = part.Inline;
            model.SmallRadios = part.SmallRadios;
            model.SessionKey = part.SessionKey;

            model.GdsRadioGroupPart = part;
            model.BuildPartDisplayContext = context;

            return Task.CompletedTask;
        }
    }
}
