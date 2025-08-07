using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using System.Text.Encodings.Web;
using OrchardCore.Workflows.Services;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class CreateSessionVariablesTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        private readonly JavaScriptEncoder _javaScriptEncoder;
        private readonly IStringLocalizer S;

        public CreateSessionVariablesTask(
            IHttpContextAccessor httpContextAccessor,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IStringLocalizer<CreateSessionVariablesTask> localizer,
            JavaScriptEncoder javaScriptEncoder
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _expressionEvaluator = expressionEvaluator;
            _javaScriptEncoder = javaScriptEncoder;
            S = localizer;
        }

        public override string Name => nameof(CreateSessionVariablesTask);

        public override LocalizedString DisplayText => S["Create Session Variables Task"];

        public override LocalizedString Category => S["Session"];

        public WorkflowExpression<string> ContentProperties
        {
            get => GetProperty(() => new WorkflowExpression<string>(JsonConvert.SerializeObject(new { DisplayText = S["Enter a title"].Value }, Formatting.Indented)));
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Failed"]);
        }

        public async override Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            if (!String.IsNullOrWhiteSpace(ContentProperties.Expression))
            {
                var contentProperties = await _expressionEvaluator.EvaluateAsync(ContentProperties, workflowContext, _javaScriptEncoder);
                var objSession = JObject.Parse(contentProperties);

                foreach (var item in objSession)
                {
                    _httpContextAccessor.HttpContext?.Session?.SetString(item.Key, item.Value?.Value<string>());
                }


                return Outcomes("Done");
            }

            return Outcomes("Failed");

        }
    }
}
