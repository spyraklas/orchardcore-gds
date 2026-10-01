using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrchardCore.GDS.Session.Cookie.Helpers;
using OrchardCore.GDS.Session.Helpers;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using System.Text.Encodings.Web;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class CreateSessionVariablesTask : TaskActivity
    {
       // private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        private readonly JavaScriptEncoder _javaScriptEncoder;
        private readonly IStringLocalizer S;
        private readonly ISessionHelper _sessionHelper;

        public CreateSessionVariablesTask(
           // IHttpContextAccessor httpContextAccessor,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IStringLocalizer<CreateSessionVariablesTask> localizer,
            JavaScriptEncoder javaScriptEncoder,
            ISessionHelper sessionHelper
        )
        {
           // _httpContextAccessor = httpContextAccessor;
            _expressionEvaluator = expressionEvaluator;
            _javaScriptEncoder = javaScriptEncoder;
            _sessionHelper = sessionHelper;
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
                    var value = item.Value?.Value<string>();
                   // _httpContextAccessor.HttpContext?.Session?.SetString(item.Key, value);
                    _sessionHelper.SetSessionValue(item.Key, value);
                }


                return Outcomes("Done");
            }

            return Outcomes("Failed");

        }
    }
}
