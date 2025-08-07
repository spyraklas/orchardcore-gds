using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsSelectPartDisplayDriver : ContentPartDisplayDriver<GdsSelectPart>
    {
        public override IDisplayResult Display(GdsSelectPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsSelectPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsSelectPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsSelectPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.SessionKey = part.SessionKey;

                model.ContentItem = part.ContentItem;
                model.GdsSelectPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsSelectPart part, IUpdateModel updater)
        {
            var viewModel = new GdsSelectPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.SessionKey = viewModel.SessionKey?.Trim();
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsSelectPartViewModel model, GdsSelectPart part)
        {
            model.ContentItem = part.ContentItem;

            model.SessionKey = part.SessionKey;

            model.GdsSelectPart = part;

            return Task.CompletedTask;
        }
    }
}
