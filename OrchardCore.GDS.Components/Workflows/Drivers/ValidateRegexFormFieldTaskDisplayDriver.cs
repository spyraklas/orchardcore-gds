using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class ValidateRegexFormFieldTaskDisplayDriver : ActivityDisplayDriver<ValidateRegexFormFieldTask, ValidateRegexFormFieldTaskViewModel>
    {
        protected override void EditActivity(ValidateRegexFormFieldTask activity, ValidateRegexFormFieldTaskViewModel model)
        {
            model.FieldName = activity.FieldName;
            model.ErrorMessage = activity.ErrorMessage;
            model.Regex = activity.Regex;
        }

        protected override void UpdateActivity(ValidateRegexFormFieldTaskViewModel model, ValidateRegexFormFieldTask activity)
        {
            activity.FieldName = model.FieldName?.Trim();
            activity.ErrorMessage = model.ErrorMessage?.Trim();
            activity.Regex = model.Regex?.Trim();
        }
    }
}
