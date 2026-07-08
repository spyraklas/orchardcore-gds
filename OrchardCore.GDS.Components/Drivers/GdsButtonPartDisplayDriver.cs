using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsButtonPartDisplayDriver : ContentPartDisplayDriver<GdsButtonPart>
    {
        public override IDisplayResult Display(GdsButtonPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsButtonPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsButtonPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsButtonPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Type = part.Type;
                model.Style = part.Style;
                model.Value = part.Value;
                model.Class = part.Class;
                model.Href = part.Href;
                model.Target = part.Target;
                model.OnClick = part.OnClick;
                model.Disabled = part.Disabled;
                model.PreventDoubleClick = part.PreventDoubleClick;

                model.ContentItem = part.ContentItem;
                model.GdsButtonPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsButtonPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsButtonPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Type = viewModel.Type?.Trim();
                part.Style = viewModel.Style?.Trim();
                part.Value = viewModel.Value?.Trim();
                part.Class = viewModel.Class?.Trim();
                part.Href = viewModel.Href?.Trim();
                part.Target = viewModel.Target?.Trim();
                part.OnClick = viewModel.OnClick?.Trim();
                part.Disabled = viewModel.Disabled;
                part.PreventDoubleClick = viewModel.PreventDoubleClick;
            }

            return await EditAsync(part, context);
        }

        private Task BuildViewModel(GdsButtonPartViewModel model, GdsButtonPart part, BuildPartDisplayContext context)
        {
            model.ContentItem = part.ContentItem;
            model.Type = part.Type;
            model.Style = part.Style;
            model.Value = part.Value;
            model.Class = part.Class;
            model.Href = part.Href;
            model.Target = part.Target;
            model.OnClick = part.OnClick;
            model.Disabled = part.Disabled;
            model.PreventDoubleClick = part.PreventDoubleClick;
            model.GdsButtonPart = part;

            return Task.CompletedTask;
        }
    }
}

