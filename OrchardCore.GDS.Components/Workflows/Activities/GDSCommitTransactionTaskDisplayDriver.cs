using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Display;
using OrchardCore.Workflows.Drivers;
using OrchardCore.Workflows.ViewModels;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class GDSCommitTransactionTaskDisplayDriver : ActivityDisplayDriver<CommitTransactionTask, CommitTransactionTaskViewModel>
    {
        protected override void EditActivity(CommitTransactionTask activity, CommitTransactionTaskViewModel model)
        {
        }

        protected override void UpdateActivity(CommitTransactionTaskViewModel model, CommitTransactionTask activity)
        {
        }
    }
}
