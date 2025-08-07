using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsCheckboxGroupPartDisplayDriver : ContentPartDisplayDriver<GdsCheckboxGroupPart>
    {
        public override IDisplayResult Display(GdsCheckboxGroupPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsCheckboxGroupPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsCheckboxGroupPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsCheckboxGroupPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.LegendSize = part.LegendSize;
                model.LegendStyle = part.LegendStyle;
                model.SmallCheckboxes = part.SmallCheckboxes;
                model.SessionKey = part.SessionKey;

                model.ContentItem = part.ContentItem;
                model.GdsCheckboxGroupPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsCheckboxGroupPart part, IUpdateModel updater)
        {
            var viewModel = new GdsCheckboxGroupPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.LegendSize = viewModel.LegendSize?.Trim();
                part.LegendStyle = viewModel.LegendStyle?.Trim();
                part.SmallCheckboxes = viewModel.SmallCheckboxes;
                part.SessionKey = viewModel.SessionKey?.Trim();
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsCheckboxGroupPartViewModel model, GdsCheckboxGroupPart part, BuildPartDisplayContext context)
        {
            model.ContentItem = part.ContentItem;

            model.LegendSize = part.LegendSize;
            model.LegendStyle = part.LegendStyle;
            model.SmallCheckboxes = part.SmallCheckboxes;
            model.SessionKey = part.SessionKey;

            model.GdsCheckboxGroupPart = part;
            model.BuildPartDisplayContext = context;

            return Task.CompletedTask;
        }
    }
}
