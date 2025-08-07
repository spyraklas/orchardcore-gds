using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class ValidateWordCountTaskDisplayDriver : ActivityDisplayDriver<ValidateWordCountTask, ValidateWordCountTaskViewModel>
    {
        protected override void EditActivity(ValidateWordCountTask activity, ValidateWordCountTaskViewModel model)
        {
            model.FieldName = activity.FieldName;
            model.ErrorMessage = activity.ErrorMessage;
            model.Count = activity.Count;
        }

        protected override void UpdateActivity(ValidateWordCountTaskViewModel model, ValidateWordCountTask activity)
        {
            activity.FieldName = model.FieldName?.Trim();
            activity.ErrorMessage = model.ErrorMessage?.Trim();
            activity.Count = model.Count;
        }
    }
}
