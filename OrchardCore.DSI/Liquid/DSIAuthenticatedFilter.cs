using Fluid;
using Fluid.Values;
using OrchardCore.DSI.Core;
using OrchardCore.Liquid;

namespace OrchardCore.DSI.Liquid
{
    internal class DSIAuthenticatedFilter : ILiquidFilter
    {
        private readonly IDSIUserHandler _dsiUserHandler;
        public DSIAuthenticatedFilter(IDSIUserHandler dsiUserHandler)
        {
            _dsiUserHandler = dsiUserHandler;
        }

        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
        {
            var userroles = input.ToStringValue();

            if(!await _dsiUserHandler.IsAuthorized())
            {
                return BooleanValue.Create(false);
            }

            return BooleanValue.Create(await _dsiUserHandler.IsAuthenticated(userroles));
        }
    }
}