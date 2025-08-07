using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using OrchardCore.GDS.Components.Handlers;
using OrchardCore.GDS.Components.Models;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class RemoveBlobFileTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        private readonly IBlobFilesHandler _blobFilesHandler;
        private readonly IStringLocalizer S;

        public RemoveBlobFileTask(
            IHttpContextAccessor httpContextAccessor,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IBlobFilesHandler blobFilesHandler,
            IStringLocalizer<RemoveBlobFileTask> localizer
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _expressionEvaluator = expressionEvaluator;
            _blobFilesHandler = blobFilesHandler;
            S = localizer;
        }

        public override string Name => nameof(RemoveBlobFileTask);

        public override LocalizedString DisplayText => S["Remove Blob File Task"];

        public override LocalizedString Category => S["Files"];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Failed"]);
        }

        public async override Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            string filePath = _httpContextAccessor.HttpContext.Request.Form["filepath"];
            if (!string.IsNullOrEmpty(filePath))
            {
                bool deleted = await _blobFilesHandler.DeleteAsync(filePath);

                if (!deleted)
                {
                    return Outcomes("Failed");
                }
            }

            return Outcomes("Done");
        }
    }
}
