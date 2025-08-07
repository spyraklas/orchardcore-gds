using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class ValidateNumberOfFilesTaskDisplayDriver : ActivityDisplayDriver<ValidateNumberOfFilesTask, ValidateNumberOfFilesTaskViewModel>
    {
        protected override void EditActivity(ValidateNumberOfFilesTask activity, ValidateNumberOfFilesTaskViewModel model)
        {
            model.FieldName = activity.FieldName;
            model.ErrorMessage = activity.ErrorMessage;
            model.SessionKey = activity.SessionKey;
            model.MaxNumberOfFiles = activity.MaxNumberOfFiles;
        }

        protected override void UpdateActivity(ValidateNumberOfFilesTaskViewModel model, ValidateNumberOfFilesTask activity)
        {
            activity.FieldName = model.FieldName?.Trim();
            activity.ErrorMessage = model.ErrorMessage?.Trim();
            activity.SessionKey = model.SessionKey?.Trim();
            activity.MaxNumberOfFiles = model.MaxNumberOfFiles;
        }
    }
}
