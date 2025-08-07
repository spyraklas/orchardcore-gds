using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class ValidateCharacterCountTaskDisplayDriver : ActivityDisplayDriver<ValidateCharacterCountTask, ValidateCharacterCountTaskViewModel>
    {
        protected override void EditActivity(ValidateCharacterCountTask activity, ValidateCharacterCountTaskViewModel model)
        {
            model.FieldName = activity.FieldName;
            model.ErrorMessage = activity.ErrorMessage;
            model.Count = activity.Count;
        }

        protected override void UpdateActivity(ValidateCharacterCountTaskViewModel model, ValidateCharacterCountTask activity)
        {
            activity.FieldName = model.FieldName?.Trim();
            activity.ErrorMessage = model.ErrorMessage?.Trim();
            activity.Count = model.Count;
        }
    }
}
