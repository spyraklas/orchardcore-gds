using Fluid;
using Fluid.Values;
using OrchardCore.GDS.Session.Helpers;
using OrchardCore.Liquid;

namespace OrchardCore.GDS.Components.Liquid
{
    public class SessionFilter : ILiquidFilter
    {
        private readonly ISessionHelper _sessionHelper;

        public SessionFilter(ISessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
        {
            var sessionKey = input.ToStringValue();

            return new StringValue(_sessionHelper.GetSessionValue(sessionKey));
        }
    }
}
