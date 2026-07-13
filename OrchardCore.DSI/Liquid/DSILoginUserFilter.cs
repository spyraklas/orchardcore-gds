using Fluid;
using Fluid.Values;
using OrchardCore.DSI.Core;
using OrchardCore.DSI.Core.Models;
using OrchardCore.Liquid;

namespace OrchardCore.DSI.Liquid
{
    public class DSILoginUserFilter : ILiquidFilter
    {
        private readonly IDSIUserHandler _dsiUserHandler;

        public DSILoginUserFilter(IDSIUserHandler dsiUserHandler)
        {
            _dsiUserHandler = dsiUserHandler;
        }

        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
        {
            if(!await _dsiUserHandler.IsAuthorized())
            {
                return ObjectValue.Create(null, new TemplateOptions());
            }

            User user = await _dsiUserHandler.GetLoginUser();

            return ObjectValue.Create(user, context.Options);
        }
    }
}