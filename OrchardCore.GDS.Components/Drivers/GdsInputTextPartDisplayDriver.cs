using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsInputTextPartDisplayDriver : ContentPartDisplayDriver<GdsInputTextPart>
    {
        public override IDisplayResult Display(GdsInputTextPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsInputTextPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsInputTextPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsInputTextPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Placeholder = part.Placeholder;
                model.Value = part.Value;
                model.SpellCheck = part.SpellCheck;
                model.Prefix = part.Prefix;
                model.Suffix = part.Suffix;
                model.Type = part.Type;
                model.AutoComplete = part.AutoComplete;
                model.SessionKey = part.SessionKey;

                model.ContentItem = part.ContentItem;
                model.GdsTextInputPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsInputTextPart part, IUpdateModel updater)
        {
            var viewModel = new GdsInputTextPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Placeholder = viewModel.Placeholder?.Trim();
                part.Value = viewModel.Value?.Trim();
                part.SpellCheck = viewModel.SpellCheck;
                part.Prefix = viewModel.Prefix?.Trim();
                part.Suffix = viewModel.Suffix?.Trim();
                part.Type = viewModel.Type?.Trim();
                part.AutoComplete = viewModel.AutoComplete?.Trim();
                part.SessionKey = viewModel.SessionKey?.Trim();
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsInputTextPartViewModel model, GdsInputTextPart part)
        {
            model.ContentItem = part.ContentItem;

            model.Placeholder = part.Placeholder;
            model.Value = part.Value;
            model.SpellCheck = part.SpellCheck;
            model.Prefix = part.Prefix;
            model.Suffix = part.Suffix;
            model.Type = part.Type;
            model.AutoComplete = part.AutoComplete;
            model.SessionKey = part.SessionKey;

            model.GdsTextInputPart = part;

            return Task.CompletedTask;
        }
    }
}
