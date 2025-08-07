using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GDS.Components.Models;
using OrchardCore.GDS.Components.ViewModels;

namespace OrchardCore.GDS.Components.Drivers
{
    public class GdsBreadcrumbPartDisplayDriver : ContentPartDisplayDriver<GdsBreadcrumbPart>
    {
        public override IDisplayResult Edit(GdsBreadcrumbPart part, BuildPartEditorContext context)
        {
            return Initialize<GdsBreadcrumbPartViewModel>("GdsBreadcrumbPart_Fields_Edit", model =>
            {
                model.Href = part.Href;
                model.Value = part.Value;

                model.ContentItem = part.ContentItem;
                model.GdsBreadcrumbPart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GdsBreadcrumbPart part, IUpdateModel updater)
        {
            var viewModel = new GdsBreadcrumbPartViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Href = viewModel.Href?.Trim();
                part.Value = viewModel.Value?.Trim();
            }

            return Edit(part);
        }
    }
}

