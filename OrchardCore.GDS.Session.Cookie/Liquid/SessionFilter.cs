using Fluid;
using Fluid.Values;
using Microsoft.AspNetCore.Http;
using OrchardCore.GDS.Session.Helpers;
using OrchardCore.Liquid;

namespace OrchardCore.GDS.Session.Cookie.Liquid;

public class SessionFilter : ILiquidFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISessionHelper _sessionHelper;

    public SessionFilter(IHttpContextAccessor httpContextAccessor, ISessionHelper sessionHelper)
    {
        _httpContextAccessor = httpContextAccessor;
        _sessionHelper = sessionHelper;
    }

    public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
    {
        var sessionKey = input.ToStringValue();

        return new StringValue(_sessionHelper.GetSessionValue(sessionKey));
    }
}