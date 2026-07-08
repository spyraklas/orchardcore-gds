using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsAccordionSectionPartDisplayDriver : ContentPartDisplayDriver<GdsAccordionSectionPart>
    {
        public override IDisplayResult Edit(GdsAccordionSectionPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsAccordionSectionPartViewModel>("GdsAccordionSectionPart_Fields_Edit", model =>
            {
                model.Id = part.Id;
                model.Class = part.Class;
                model.Title = part.Title;
                model.TitleStyle = part.TitleStyle;
                model.TitleClass = part.TitleClass;
                model.Summary = part.Summary;
                model.SummaryClass = part.SummaryClass;

                model.ContentItem = part.ContentItem;
                model.GdsAccordionSectionPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsAccordionSectionPart part, UpdatePartEditorContext context)
        {
            var viewModel = new GdsAccordionSectionPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Id = viewModel.Id?.Trim();
                part.Class = viewModel.Class?.Trim();
                part.Title = viewModel.Title?.Trim();
                part.TitleStyle = viewModel.TitleStyle?.Trim();
                part.TitleClass = viewModel.TitleClass?.Trim();
                part.Summary = viewModel.Summary?.Trim();
                part.SummaryClass = viewModel.SummaryClass?.Trim();
            }

            return await EditAsync(part, context);
        }
    }
}
