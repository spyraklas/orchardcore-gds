using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class ValidateGdsDateInputTaskDisplayDriver : ActivityDisplayDriver<ValidateGdsDateInputTask, ValidateGdsDateInputTaskViewModel>
    {
        protected override void EditActivity(ValidateGdsDateInputTask activity, ValidateGdsDateInputTaskViewModel model)
        {
            model.FieldName = activity.FieldName;
            model.ErrorMessage = activity.ErrorMessage;
            model.FieldTitle = activity.FieldTitle;
        }

        protected override void UpdateActivity(ValidateGdsDateInputTaskViewModel model, ValidateGdsDateInputTask activity)
        {
            activity.FieldName = model.FieldName?.Trim();
            activity.ErrorMessage = model.ErrorMessage?.Trim();
            activity.FieldTitle = model.FieldTitle?.Trim();
        }
    }
}
