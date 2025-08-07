using OrchardCore.GDS.Components.Workflows.Activities;
using OrchardCore.GDS.Components.Workflows.ViewModels;
using OrchardCore.Workflows.Display;
using OrchardCore.Workflows.Models;

namespace OrchardCore.GDS.Components.Workflows.Drivers
{
    public class CreateSessionVariablesTaskDisplayDriver : ActivityDisplayDriver<CreateSessionVariablesTask, CreateSessionVariablesTaskViewModel>
    {
        protected override void EditActivity(CreateSessionVariablesTask activity, CreateSessionVariablesTaskViewModel model)
        {
            model.ContentProperties = activity.ContentProperties.Expression;
        }

        protected override void UpdateActivity(CreateSessionVariablesTaskViewModel model, CreateSessionVariablesTask activity)
        {
            activity.ContentProperties = new WorkflowExpression<string>(model.ContentProperties);
        }
    }
}
