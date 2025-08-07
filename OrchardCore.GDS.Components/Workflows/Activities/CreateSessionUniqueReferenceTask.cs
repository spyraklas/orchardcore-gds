using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class CreateSessionUniqueReferenceTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer S;

        public CreateSessionUniqueReferenceTask(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<CreateSessionUniqueReferenceTask> localizer
        )
        {
            _httpContextAccessor = httpContextAccessor;
            S = localizer;
        }

        public override string Name => nameof(CreateSessionUniqueReferenceTask);

        public override LocalizedString DisplayText => S["Create Session Unique Reference Task"];

        public override LocalizedString Category => S["Session"];

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
            if (!String.IsNullOrWhiteSpace(SessionKey))
            {
                string refference = DateTime.Now.Ticks.ToString("x").ToUpper();

                _httpContextAccessor.HttpContext?.Session?.SetString(SessionKey, refference);

                return await Task.FromResult(Outcomes("Done"));
            }

            return await Task.FromResult(Outcomes("Failed"));

        }
    }
}
