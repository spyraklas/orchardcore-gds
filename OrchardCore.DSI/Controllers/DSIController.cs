using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.DSI.Core.Constants;
using OrchardCore.Environment.Shell;

namespace OrchardCore.DSI.Controllers
{
    [Authorize(AuthenticationSchemes = ApplicationConstants.DSIClientScheme)]
    public class DSIController : Controller
    {
        private readonly ShellSettings _shellSettings;

        public DSIController(ShellSettings shellSettings) 
        { 
            _shellSettings = shellSettings;
        }

        [HttpGet]
        public IActionResult Login(string redirectUrl)
        {
            //var redirectCompleteUrl = $"/{_shellSettings.RequestUrlPrefix}/dsi/LoginComplete/?redirectUrl={redirectUrl}"; 
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, ApplicationConstants.DSIClientScheme);
        }

        [HttpGet]
        public IActionResult Logout(string redirectUrl)
        {
            return SignOut(new AuthenticationProperties { RedirectUri = redirectUrl },
                ApplicationConstants.DSIClientCookieScheme, ApplicationConstants.DSIClientScheme);
        }
    }
}
