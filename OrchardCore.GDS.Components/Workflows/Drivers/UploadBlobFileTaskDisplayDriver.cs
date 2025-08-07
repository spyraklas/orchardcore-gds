using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class UploadBlobFileTaskDisplayDriver : ActivityDisplayDriver<UploadBlobFileTask, UploadBlobFileTaskViewModel>
    {
        protected override void EditActivity(UploadBlobFileTask activity, UploadBlobFileTaskViewModel model)
        {
            model.SessionKey = activity.SessionKey;
        }

        protected override void UpdateActivity(UploadBlobFileTaskViewModel model, UploadBlobFileTask activity)
        {
            activity.SessionKey = model.SessionKey?.Trim();
        }
    }
}
