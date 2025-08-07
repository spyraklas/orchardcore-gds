using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsBreadcrumbsPartDisplayDriver : ContentPartDisplayDriver<GdsBreadcrumbsPart>
    {
        public override IDisplayResult Display(GdsBreadcrumbsPart part, BuildPartDisplayContext context)
        {
            return Initialize<GdsBreadcrumbsPartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content")
                .Location("Summary", "Content")
                ;
        }

        public override IDisplayResult Edit(GdsBreadcrumbsPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsBreadcrumbsPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.MobileColapse = part.MobileColapse;

                model.ContentItem = part.ContentItem;
                model.GdsBreadcrumbsPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsBreadcrumbsPart part, IUpdateModel updater)
        {
            var viewModel = new GdsBreadcrumbsPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.MobileColapse = viewModel.MobileColapse;
            }

            return Edit(part);
        }

        private Task BuildViewModel(GdsBreadcrumbsPartViewModel model, GdsBreadcrumbsPart part, BuildPartDisplayContext context)
        {
            model.BuildPartDisplayContext = context;
            model.ContentItem = part.ContentItem;
            model.MobileColapse = part.MobileColapse;
            model.GdsBreadcrumbsPart = part;

            return Task.CompletedTask;
        }
    }
}

