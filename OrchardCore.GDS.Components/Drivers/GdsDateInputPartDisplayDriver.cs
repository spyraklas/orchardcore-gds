using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsDateInputPartDisplayDriver : ContentPartDisplayDriver<GdsDateInputPart>
    {
        public override IDisplayResult Display(GdsDateInputPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsDateInputPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsDateInputPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsDateInputPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.DayValue = part.DayValue;
                model.MonthValue = part.MonthValue;
                model.YearValue = part.YearValue;
                model.AutoComplete = part.AutoComplete;
                model.DaySessionKey = part.DaySessionKey;
                model.MonthSessionKey = part.MonthSessionKey;
                model.YearSessionKey = part.YearSessionKey;

                model.ContentItem = part.ContentItem;
                model.GdsDateInputPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsDateInputPart part, IUpdateModel updater)
        {
            var viewModel = new GdsDateInputPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.DayValue = viewModel.DayValue?.Trim();
                part.MonthValue = viewModel.MonthValue?.Trim();
                part.YearValue = viewModel.YearValue;
                part.AutoComplete = viewModel.AutoComplete?.Trim();
                part.DaySessionKey = viewModel.DaySessionKey?.Trim();
                part.MonthSessionKey = viewModel.MonthSessionKey?.Trim();
                part.YearSessionKey = viewModel.YearSessionKey?.Trim();
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsDateInputPartViewModel model, GdsDateInputPart part)
        {
            model.ContentItem = part.ContentItem;

            model.DayValue = part.DayValue;
            model.MonthValue = part.MonthValue;
            model.YearValue = part.YearValue;
            model.AutoComplete = part.AutoComplete;
            model.DaySessionKey = part.DaySessionKey;
            model.MonthSessionKey = part.MonthSessionKey;
            model.YearSessionKey = part.YearSessionKey;

            model.GdsDateInputPart = part;

            return Task.CompletedTask;
        }
    }
}
