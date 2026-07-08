using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using OrchardCore.GDS.Components.Handlers;
using OrchardCore.GDS.Components.Models;
using OrchardCore.Workflows.Http;
using System.Globalization;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class DownloadBlobFileTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        private readonly IBlobFilesHandler _blobFilesHandler;
        private readonly IStringLocalizer S;

        public DownloadBlobFileTask(
            IHttpContextAccessor httpContextAccessor,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IBlobFilesHandler blobFilesHandler,
            IStringLocalizer<DownloadBlobFileTask> localizer
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _expressionEvaluator = expressionEvaluator;
            _blobFilesHandler = blobFilesHandler;
            S = localizer;
        }

        public override string Name => nameof(DownloadBlobFileTask);

        public override LocalizedString DisplayText => S["Download Blob File Task"];

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
                BlobFile file = await _blobFilesHandler.DownloadAsync(filePath);

                if (file == null)
                {
                    return Outcomes("Failed");
                }

                var response = _httpContextAccessor.HttpContext.Response;

                if(!response.Headers.ContainsKey("Content-Disposition"))
                    response.Headers.Append("Content-Disposition", "attachment; filename=" + file.OriginalName);

                if (!response.Headers.ContainsKey("Content-Length"))
                    response.Headers.Append("Content-Length", file.Content.Length.ToString(CultureInfo.InvariantCulture));

                response.ContentType = file.ContentType;
                response.StatusCode = 200;
                await response.Body.WriteAsync(file.Content);

                _httpContextAccessor.HttpContext.Items[WorkflowHttpResult.Instance] = WorkflowHttpResult.Instance;
            }
            else
            {
                return Outcomes("Failed");
            }

            return Outcomes("Done");
        }
    }
}
