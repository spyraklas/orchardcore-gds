using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using OrchardCore.GDS.Components.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class ValidateNumberOfFilesTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        private readonly IBlobFilesHandler _blobFilesHandler;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IStringLocalizer S;

        public ValidateNumberOfFilesTask(
            IHttpContextAccessor httpContextAccessor,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IBlobFilesHandler blobFilesHandler,
            IUpdateModelAccessor updateModelAccessor,
            IStringLocalizer<ValidateNumberOfFilesTask> localizer
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _expressionEvaluator = expressionEvaluator;
            _blobFilesHandler = blobFilesHandler;
            _updateModelAccessor = updateModelAccessor;
            S = localizer;
        }

        public override string Name => nameof(ValidateNumberOfFilesTask);

        public override LocalizedString DisplayText => S["Upload Blob File Task"];

        public override LocalizedString Category => S["Files"];

        public string FieldName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string ErrorMessage
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string SessionKey
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public int MaxNumberOfFiles
        {
            get => GetProperty(() => 0);
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Failed"]);
        }

        public async override Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            string sessionValue = _httpContextAccessor.HttpContext.Session.GetString(SessionKey);

            if (!string.IsNullOrEmpty(sessionValue) && MaxNumberOfFiles > 0)
            {
                var list = await _blobFilesHandler.GetStoredFilesAsync(sessionValue);

                if(list.Count + 1 > MaxNumberOfFiles)
                {
                    var updater = _updateModelAccessor.ModelUpdater;

                    if (updater != null)
                    {
                        updater.ModelState.TryAddModelError(FieldName, ErrorMessage);
                    }

                    return Outcomes("Failed");
                }
            }

            return Outcomes("Done");
        }
    }
}
