using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsTextareaPartDisplayDriver : ContentPartDisplayDriver<GdsTextareaPart>
    {
        public override IDisplayResult Display(GdsTextareaPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsTextareaPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsTextareaPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsTextareaPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Value = part.Value;
                model.SpellCheck = part.SpellCheck;
                model.Rows = part.Rows;
                model.Count = part.Count;
                model.CountWords = part.CountWords;
                model.SessionKey = part.SessionKey;

                model.ContentItem = part.ContentItem;
                model.GdsTextareaPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsTextareaPart part, IUpdateModel updater)
        {
            var viewModel = new GdsTextareaPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Value = viewModel.Value?.Trim();
                part.SpellCheck = viewModel.SpellCheck;
                part.Count = viewModel.Count;
                part.CountWords = viewModel.CountWords;
                part.Rows = viewModel.Rows;
                part.SessionKey = viewModel.SessionKey?.Trim();
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsTextareaPartViewModel model, GdsTextareaPart part)
        {
            model.ContentItem = part.ContentItem;

            model.Value = part.Value;
            model.SpellCheck = part.SpellCheck;
            model.Rows = part.Rows;
            model.Count = part.Count;
            model.CountWords = part.CountWords;
            model.SessionKey = part.SessionKey;

            model.GdsTextareaPart = part;

            return Task.CompletedTask;
        }
    }
}
