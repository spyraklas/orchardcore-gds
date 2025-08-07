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
    public class ValidateGdsDateInputTask : TaskActivity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IStringLocalizer S;
        private readonly string numbersOnlyRegExPattern = "^[0-9]*$";

        public ValidateGdsDateInputTask(
            IHttpContextAccessor httpContextAccessor,
            IUpdateModelAccessor updateModelAccessor,
            IStringLocalizer<ValidateGdsDateInputTask> localizer
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _updateModelAccessor = updateModelAccessor;
            S = localizer;
        }

        public override string Name => nameof(ValidateGdsDateInputTask);

        public override LocalizedString DisplayText => S["Validate GDS Date Input Form Field Task"];

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

        public string FieldTitle
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
            var fieldDayId = FieldName + "-day";
            var fieldMonthId = FieldName + "-month";
            var fieldYearId = FieldName + "-year";

            var form = _httpContextAccessor.HttpContext.Request.Form;
            var fieldDayValue = form[fieldDayId];
            var fieldMonthValue = form[fieldMonthId];
            var fieldYearValue = form[fieldYearId];
            var outcome = "Valid";

            if (!IsDayValid(fieldDayValue) || !IsMonthValid(fieldMonthValue) || !IsYearValid(fieldYearValue))
            {
                var updater = _updateModelAccessor.ModelUpdater;
                outcome = "InValid";

                if (updater != null)
                {
                    //all empty
                    if (string.IsNullOrEmpty(fieldDayValue) && string.IsNullOrEmpty(fieldMonthValue) && string.IsNullOrEmpty(fieldYearValue))
                    {
                        updater.ModelState.TryAddModelError(FieldName, ErrorMessage);
                        return Outcomes("Done", outcome);
                    }

                    //validate day
                    if (string.IsNullOrEmpty(fieldDayValue))
                    {
                        var errorMessage = $"{FieldTitle} must include a day";
                        updater.ModelState.TryAddModelError(fieldDayId, errorMessage);
                    }
                    else if (!IsDayValid(fieldDayValue))
                    {
                        var errorMessage = $"{FieldTitle} must be a real date";
                        updater.ModelState.TryAddModelError(fieldDayId, errorMessage);

                        return Outcomes("Done", outcome);
                    }

                    //validate month
                    if (string.IsNullOrEmpty(fieldMonthValue))
                    {
                        var errorMessage = $"{FieldTitle} must include a month";
                        updater.ModelState.TryAddModelError(fieldMonthId, errorMessage);
                    }
                    else if (!IsMonthValid(fieldMonthValue))
                    {
                        var errorMessage = $"{FieldTitle} must be a real date";
                        updater.ModelState.TryAddModelError(fieldMonthId, errorMessage);

                        return Outcomes("Done", outcome);
                    }

                    //validate year
                    if (string.IsNullOrEmpty(fieldYearValue))
                    {
                        var errorMessage = $"{FieldTitle} must include a year";
                        updater.ModelState.TryAddModelError(fieldYearId, errorMessage);
                    }
                    else if (!IsYearValid(fieldYearValue))
                    {
                        var errorMessage = $"{FieldTitle} must be a real date";
                        updater.ModelState.TryAddModelError(fieldYearId, errorMessage);

                        return Outcomes("Done", outcome);
                    }
                }

                return Outcomes("Done", outcome);
            }
  
            if (!DateTime.TryParse($"{fieldDayValue}-{fieldMonthValue}-{fieldYearValue}", out DateTime dateValue))
            {
                var updater = _updateModelAccessor.ModelUpdater;
                outcome = "InValid";
                var errorMessage = $"{FieldTitle} must be a real date";

                if (updater != null)
                {
                    updater.ModelState.TryAddModelError(FieldName, errorMessage);
                }
            }

            return Outcomes("Done", outcome);
        }

        private bool IsDayValid(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex validationRegex = new Regex(numbersOnlyRegExPattern);
                if (validationRegex.IsMatch(value))
                {
                    int dayNumber = Convert.ToInt32(value);

                    return dayNumber >= 1 && dayNumber <= 31;
                }
            }

            return false;
        }

        private bool IsMonthValid(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex validationRegex = new Regex(numbersOnlyRegExPattern);
                if (validationRegex.IsMatch(value))
                {
                    int monthNumber = Convert.ToInt32(value);

                    return monthNumber >= 1 && monthNumber <= 12;
                }
            }

            return false;
        }

        private bool IsYearValid(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex validationRegex = new Regex(numbersOnlyRegExPattern);
                if (validationRegex.IsMatch(value))
                {
                    int yearNumber = Convert.ToInt32(value);

                    return yearNumber >= 1;
                }
            }

            return false;
        }
    }
}
