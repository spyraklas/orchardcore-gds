using OrchardCore.DSI.Workflows.Activities;
using OrchardCore.DSI.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.DSI.Workflows.Drivers
{
    public class ValidateGdsUserTaskDisplayDriver : ActivityDisplayDriver<ValidateGdsUserTask, ValidateGdsUserTaskViewModel>
    {
        protected override void EditActivity(ValidateGdsUserTask activity, ValidateGdsUserTaskViewModel model)
        {
            model.UserRoles = activity.UserRoles;
        }

        protected override void UpdateActivity(ValidateGdsUserTaskViewModel model, ValidateGdsUserTask activity)
        {
            activity.UserRoles = model.UserRoles?.Trim();
        }
    }
}
