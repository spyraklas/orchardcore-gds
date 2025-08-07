using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Flows.Models;
using OrchardCore.Recipes;
using OrchardCore.Recipes.Services;

namespace OrchardCore.GDS.Components
{
    public sealed class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IRecipeMigrator _recipeMigrator;
        private readonly ILogger<Migrations> _logger;

        public Migrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator, ILogger<Migrations> logger)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
            _logger = logger;
        }

        public async Task<int> CreateAsync()
        {
            //GdsCssPart
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsCssPart", builder => builder
                .Attachable()
                .WithDescription("Provides a GDS css attributes part for your content item."));

            //GdsJsPart
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsJsPart", builder => builder
                .Attachable()
                .WithDescription("Provides a GDS js attributes part for your content item."));

            //GdsLabelPart
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsLabelPart", builder => builder
                .Attachable()
                .WithDescription("Provides a GDS label part for your content item."));

            //GdsHintPart
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsHintPart", builder => builder
                .Attachable()
                .WithDescription("Provides a GDS hint part for your content item."));

            //GdsLegendPart
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsLegendPart", builder => builder
                .Attachable()
                .WithDescription("Provides a GDS legend part for your content item."));

            //GdsInputText
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsInputTextPart", builder => builder
                .WithDescription("Provides a GDS input text part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsInputText", type => type
                .WithPart("FormInputElementPart")
                .WithPart("FormElementPart")
                .WithPart("GdsLabelPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsInputTextPart")
                .Stereotype("Widget"));

            //GdsTextarea
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsTextareaPart", builder => builder
                .WithDescription("Provides a GDS textarea part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsTextarea", type => type
                .WithPart("FormInputElementPart")
                .WithPart("FormElementPart")
                .WithPart("GdsLabelPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsTextareaPart")
                .Stereotype("Widget"));

            //GdsErrorSummary
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsErrorSummaryPart", builder => builder
               .WithDescription("Provides a GDS error summary part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsErrorSummary", type => type
                .WithPart("FormElementPart")
                .WithPart("GdsErrorSummaryPart")
                .Stereotype("Widget"));

            //GdsButton
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsButtonPart", builder => builder
               .WithDescription("Provides a GDS button part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsButton", type => type
                .WithPart("FormElementPart")
                .WithPart("GdsButtonPart")
                .Stereotype("Widget"));

            //GdsBreadcrumb
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsBreadcrumbPart", builder => builder
                .WithDescription("Provides a GDS breadcrumb part for your content item.")
            );

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsBreadcrumb", type => type
                .WithPart("GdsBreadcrumbPart")
            );

            //GdsBreadcrumbs
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsBreadcrumbsPart", builder => builder
                .WithDescription("Provides a GDS breadcrumbs part for your content item.")
            );

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsBreadcrumbs", type => type
                .WithPart("GdsBreadcrumbsPart")
                .WithPart("BagPart", part => part
                    .WithPosition("2")
                    .WithSettings(new BagPartSettings() { ContainedContentTypes = new string[] { "GdsBreadcrumb" } })
                )
                .Stereotype("Widget")
            );

            //GdsCheckbox
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsCheckboxPart", builder => builder
                .WithDescription("Provides a GDS checkbox part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsCheckbox", type => type
                .WithPart("FormElementPart")
                .WithPart("GdsJsPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsCheckboxPart")
                .WithPart("FlowPart", part => part
                    .WithPosition("10")
                )
            );

            //GdsCheckboxDivider
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsCheckboxDividerPart", builder => builder
                .WithDescription("Provides a GDS checkbox divider part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsCheckboxDivider", type => type
                .WithPart("GdsCssPart")
                .WithPart("GdsCheckboxDividerPart")
            );

            //GdsCheckboxGroup
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsCheckboxGroupPart", builder => builder
                .WithDescription("Provides a GDS checkbox group part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsCheckboxGroup", type => type
                .WithPart("FormElementPart")
                .WithPart("FormInputElementPart")
                .WithPart("GdsLabelPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsCheckboxGroupPart")
                .WithPart("BagPart", part => part
                    .WithPosition("2")
                    .WithSettings(new BagPartSettings() { ContainedContentTypes = new string[] { "GdsCheckbox", "GdsCheckboxDivider" } })
                )
                .Stereotype("Widget")
            );

            //GdsRadio
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsRadioPart", builder => builder
                .WithDescription("Provides a GDS radio part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsRadio", type => type
                .WithPart("FormElementPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsJsPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsRadioPart")
                .WithPart("FlowPart", part => part
                    .WithPosition("10")
                )
            );

            //GdsRadioDivider
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsRadioDividerPart", builder => builder
                .WithDescription("Provides a GDS radio divider part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsRadioDivider", type => type
                .WithPart("GdsCssPart")
                .WithPart("GdsRadioDividerPart")
            );

            //GdsRadioGroup
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsRadioGroupPart", builder => builder
                .WithDescription("Provides a GDS radio group part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsRadioGroup", type => type
                .WithPart("FormElementPart")
                .WithPart("FormInputElementPart")
                .WithPart("GdsLabelPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsRadioGroupPart")
                .WithPart("BagPart", part => part
                    .WithPosition("2")
                    .WithSettings(new BagPartSettings() { ContainedContentTypes = new string[] { "GdsRadio", "GdsRadioDivider" } })
                )
                .Stereotype("Widget")
            );


            //GdsAccordionSection
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsAccordionSectionPart", builder => builder
                .WithDescription("Provides a GDS accordion section part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsAccordionSection", type => type
                .WithPart("GdsAccordionSectionPart")
                .WithPart("FlowPart", part => part
                    .WithPosition("20")
                )
            );

            //GdsAccordion
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsAccordionPart", builder => builder
                .WithDescription("Provides a GDS accordion part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsAccordion", type => type
                .WithPart("GdsAccordionPart")
                .WithPart("BagPart", part => part
                    .WithPosition("3")
                    .WithSettings(new BagPartSettings() { ContainedContentTypes = new string[] { "GdsAccordionSection" } })
                )
                .Stereotype("Widget")
            );

            //GdsSelectOption
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsSelectOptionPart", builder => builder
                .WithDescription("Provides a GDS select option part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsSelectOption", type => type
                .WithPart("GdsSelectOptionPart")
            );

            //GdsSelect
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsSelectPart", builder => builder
                .WithDescription("Provides a GDS select part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsSelect", type => type
                .WithPart("FormElementPart")
                .WithPart("FormInputElementPart")
                .WithPart("GdsLabelPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsSelectPart")
                .WithPart("BagPart", part => part
                    .WithPosition("8")
                    .WithSettings(new BagPartSettings() { ContainedContentTypes = new string[] { "GdsSelectOption" } })
                )
                .Stereotype("Widget")
            );

            //GdsDateInput
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsDateInputPart", builder => builder
                .WithDescription("Provides a GDS date input part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsDateInput", type => type
                .WithPart("FormInputElementPart")
                .WithPart("FormElementPart")
                .WithPart("GdsLegendPart")
                .WithPart("GdsLabelPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsDateInputPart")
                .Stereotype("Widget"));

            //GdsUploadFiles
            await _contentDefinitionManager.AlterPartDefinitionAsync("GdsUploadFilesPart", builder => builder
                .WithDescription("Provides a GDS date input part for your content item."));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("GdsUploadFiles", type => type
                .WithPart("FormInputElementPart")
                .WithPart("FormElementPart")
                .WithPart("GdsLabelPart")
                .WithPart("GdsHintPart")
                .WithPart("GdsCssPart")
                .WithPart("GdsJsPart")
                .WithPart("GdsUploadFilesPart")
                .Stereotype("Widget"));


            return 1;
        }

        public async Task<int> UpdateFrom1Async()
        {
            string file = $@"gds{RecipesConstants.RecipeExtension}";
            
            //Add component templates
            try
            {
                await _recipeMigrator.ExecuteAsync(file, this);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while processing the recipe file {file}", ex);
                throw;
            }

            return 2;
        }
    }
}
