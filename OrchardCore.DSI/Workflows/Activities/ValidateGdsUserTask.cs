using Microsoft.Extensions.Localization;
using OrchardCore.DSI.Core;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;

namespace OrchardCore.DSI.Workflows.Activities
{
    public class ValidateGdsUserTask : TaskActivity
    {
        private readonly IDSIUserHandler _dsiUserHandler;
        private readonly IStringLocalizer S;

        public ValidateGdsUserTask(IDSIUserHandler dsiUserHandler, IStringLocalizer<ValidateGdsUserTask> localizer)
        {
            _dsiUserHandler = dsiUserHandler;
            S = localizer;
        }

        public override string Name => nameof(ValidateGdsUserTask);

        public override LocalizedString DisplayText => S["Validate GDS User access"];

        public override LocalizedString Category => S["Security"];

        public string UserRoles
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Valid"], S["Invalid"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            bool isValid = await IsValid(UserRoles);            
            var outcome = isValid ? "Valid" : "Invalid";

            return Outcomes(outcome);
        }

        private async Task<bool> IsValid(string userRoles)
        {
            if (!await _dsiUserHandler.IsAuthorized())
            {
                return false;
            }

            if (!await _dsiUserHandler.IsAuthenticated(UserRoles))
            {
                return false;
            }

            return true;
        }
    }
}
