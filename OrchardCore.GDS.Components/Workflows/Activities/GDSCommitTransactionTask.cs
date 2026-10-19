using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.GDS.Session.Helpers;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class GDSCommitTransactionTask : CommitTransactionTask
    {
        private readonly ISessionHelper _sessionHelper;
        private readonly IUpdateModelAccessor _updateModelAccessor; 

        public GDSCommitTransactionTask(
            YesSql.ISession session,
            IUpdateModelAccessor updateModelAccessor,
            IStringLocalizer<GDSCommitTransactionTask> localizer,
            ISessionHelper sessionHelper)
            : base(session, updateModelAccessor, localizer)
        {
            _sessionHelper = sessionHelper;
            _updateModelAccessor = updateModelAccessor;
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            _sessionHelper?.Save();
            _updateModelAccessor.ModelUpdater.ModelState.Clear();   
            return await base.ExecuteAsync(workflowContext, activityContext);
        }
    }
}
