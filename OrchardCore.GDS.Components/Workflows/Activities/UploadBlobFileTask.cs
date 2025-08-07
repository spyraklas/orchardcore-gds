using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using OrchardCore.GDS.Components.Handlers;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class UploadBlobFileTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        private readonly IBlobFilesHelper _blobFilesHelper;
        private readonly IStringLocalizer S;

        public UploadBlobFileTask(
            IHttpContextAccessor httpContextAccessor,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IBlobFilesHelper blobFilesHelper,
            IStringLocalizer<UploadBlobFileTask> localizer
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _expressionEvaluator = expressionEvaluator;
            _blobFilesHelper = blobFilesHelper;
            S = localizer;
        }

        public override string Name => nameof(UploadBlobFileTask);

        public override LocalizedString DisplayText => S["Upload Blob File Task"];

        public override LocalizedString Category => S["Files"];

        public string SessionKey
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }


        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Failed"]);
        }

        public async override Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            if(_httpContextAccessor.HttpContext.Request.Form.Files.Count == 1)
            {
                IFormFile formFile = _httpContextAccessor.HttpContext.Request.Form.Files[0];
             
                await _blobFilesHelper.UploadFile(formFile, GetReference());
            }

            return Outcomes("Done");
        }

        private string GetReference()
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.Session?.GetString(SessionKey)))
            {
                result = DateTime.Now.Ticks.ToString("x").ToUpper();
                _httpContextAccessor.HttpContext?.Session?.SetString(SessionKey, result);
            }
            else
            {
                result = _httpContextAccessor.HttpContext?.Session?.GetString(SessionKey);
            }

            return result;
        }
    }
}
