using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.GDS.Session.Helpers;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using System.Text.Json;

namespace OrchardCore.GDS.Components.Workflows.Activities
{
    public class GDSCommitTransactionTask : CommitTransactionTask
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISessionHelper _sessionHelper;

        public GDSCommitTransactionTask(
            YesSql.ISession session,
            IUpdateModelAccessor updateModelAccessor,
            IStringLocalizer<GDSCommitTransactionTask> localizer,
            IHttpContextAccessor httpContextAccessor,
            ISessionHelper sessionHelper)
            : base(session, updateModelAccessor, localizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _sessionHelper = sessionHelper;
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            SaveSessionItems();
            return await base.ExecuteAsync(workflowContext, activityContext);
        }

        private void SaveSessionItems()
        {
            var form = _httpContextAccessor?.HttpContext?.Request?.Form;
            if (form == null || !form.ContainsKey("__gdsComponentSessionKey"))
            {
                return;
            }   

            var sessionKeys = form?["__gdsComponentSessionKey"] 
                .Select(v => v.Split(':'))
                .ToDictionary(
                    parts => parts[0],
                    parts => parts[1]); 
                
            foreach (var item in sessionKeys)
            {
                var sessionKey = item.Value;
                var value = form[item.Key];
                _sessionHelper.SetSessionValue(sessionKey, value);
            }
        }
    }
}
