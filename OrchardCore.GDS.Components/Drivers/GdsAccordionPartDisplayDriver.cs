using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsAccordionPartDisplayDriver : ContentPartDisplayDriver<GdsAccordionPart>
    {
        public override IDisplayResult Display(GdsAccordionPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsAccordionPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsAccordionPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsAccordionPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Id = part.Id;
                model.Class = part.Class;

                model.ContentItem = part.ContentItem;
                model.GdsAccordionPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsAccordionPart part, IUpdateModel updater)
        {
            var viewModel = new GdsAccordionPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Id = viewModel.Id?.Trim();
                part.Class = viewModel.Class?.Trim();
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsAccordionPartViewModel model, GdsAccordionPart part, BuildPartDisplayContext context)
        {
            model.ContentItem = part.ContentItem;
            model.Id = part.Id;
            model.Class = part.Class;
            model.GdsAccordionPart = part;
            model.BuildPartDisplayContext = context;

            return Task.CompletedTask;
        }
    }
}
