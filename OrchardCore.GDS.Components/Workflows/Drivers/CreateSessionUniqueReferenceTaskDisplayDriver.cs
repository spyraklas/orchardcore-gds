using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class CreateSessionUniqueReferenceTaskDisplayDriver : ActivityDisplayDriver<CreateSessionUniqueReferenceTask, CreateSessionUniqueReferenceTaskViewModel>
    {
        protected override void EditActivity(CreateSessionUniqueReferenceTask activity, CreateSessionUniqueReferenceTaskViewModel model)
        {
            model.SessionKey = activity.SessionKey;
        }

        protected override void UpdateActivity(CreateSessionUniqueReferenceTaskViewModel model, CreateSessionUniqueReferenceTask activity)
        {
            activity.SessionKey = model.SessionKey?.Trim();
        }
    }
}
