using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GDS.Components.Helpers
{
    public static class GDSComponentExtensions
    {
        public static IHtmlContent GDSComponentSessionKeyPartial(this IHtmlHelper helper, string fieldName, string sessionKey)
        {
            var html = $"<input name=\"__gdsComponentSessionKey\" type=\"hidden\" value='{fieldName}:{sessionKey}'>";
            return new HtmlString(html);
        }
    }
}
