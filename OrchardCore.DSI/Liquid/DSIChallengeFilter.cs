using Fluid;
using Fluid.Values;
using Microsoft.AspNetCore.Http;
using OrchardCore.Autoroute.Models;
using OrchardCore.DSI.Core;
using OrchardCore.DSI.Models;
using OrchardCore.Environment.Shell;
using OrchardCore.Liquid;

namespace OrchardCore.DSI.Liquid
{
    public class DSIChallengeFilter : ILiquidFilter
    {
        private readonly IDSIUserHandler _dsiUserHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ShellSettings _shellSettings;

        public DSIChallengeFilter(IDSIUserHandler dsiUserHandler, IHttpContextAccessor httpContextAccessor, ShellSettings shellSettings)
        {
            _dsiUserHandler = dsiUserHandler;
            _httpContextAccessor = httpContextAccessor;
            _shellSettings = shellSettings;
        }

        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
        {
            var userroles = input.ToStringValue();
            var invalidRedirectUrl = arguments.At(0).ToStringValue();
            var autoroutePath = arguments.At(1).ToStringValue();

            var redirectUrl = $"/{_shellSettings.RequestUrlPrefix}/{autoroutePath}/";
            var redirectLoginUrl = $"/{_shellSettings.RequestUrlPrefix}/dsi/login/?redirectUrl={redirectUrl}";

            //TODO Validate with user session
            if (!await _dsiUserHandler.IsAuthorized())
            {
                _httpContextAccessor.HttpContext.Response.Redirect(redirectLoginUrl);
            }
            else if (!await _dsiUserHandler.IsAuthenticated(userroles))
            {
                _httpContextAccessor.HttpContext.Response.Redirect(invalidRedirectUrl);
            }

            return BooleanValue.Create(true);
        }
    }
}