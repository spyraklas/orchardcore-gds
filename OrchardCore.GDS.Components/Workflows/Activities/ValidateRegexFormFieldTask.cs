using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    // TODO: Add the ability to configure various types of validators.
    public class ValidateRegexFormFieldTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IStringLocalizer S;

        public ValidateRegexFormFieldTask(
            IHttpContextAccessor httpContextAccessor,
            IUpdateModelAccessor updateModelAccessor,
            IStringLocalizer<ValidateRegexFormFieldTask> localizer
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _updateModelAccessor = updateModelAccessor;
            S = localizer;
        }

        public override string Name => nameof(ValidateRegexFormFieldTask);

        public override LocalizedString DisplayText => S["Validate Form Field Regex Task"];

        public override LocalizedString Category => S["Validation"];

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

        public string Regex
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Valid"], S["Invalid"]);
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var form = _httpContextAccessor.HttpContext.Request.Form;
            var fieldValue = form[FieldName];
            var isValid = IsValid(fieldValue, Regex);
            var outcome = isValid ? "Valid" : "Invalid";

            if (!isValid)
            {
                var updater = _updateModelAccessor.ModelUpdater;

                if (updater != null)
                {
                    updater.ModelState.TryAddModelError(FieldName, ErrorMessage);
                }
            }

            return Outcomes("Done", outcome);
        }

        private bool IsValid(string value, string pattern)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(pattern))
            {
                Regex validationRegex = new Regex(pattern);
                return validationRegex.IsMatch(value);
            }

            return false;
        }
    }
}
