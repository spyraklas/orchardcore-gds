using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OrchardCore.DSI.Core.Constants;
using OrchardCore.DSI.Core.Models;

namespace OrchardCore.DSI.Core
{
    public class DSIUserHandler : IDSIUserHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;

        public DSIUserHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
        }

        public async Task<bool> IsAuthorized()
        {
            var authResult = await _httpContext.AuthenticateAsync(ApplicationConstants.DSIClientCookieScheme);

            if (authResult.Succeeded && authResult.Principal != null)
            {
                var user = authResult.Principal;
                var dsiIdentity = user?.Identity;
                return dsiIdentity != null && dsiIdentity.IsAuthenticated;
            }

            return false;
        }

        public async Task<bool> IsAuthenticated(string roles = "")
        {
            var authResult = await _httpContext.AuthenticateAsync(ApplicationConstants.DSIClientCookieScheme);

            if (authResult.Succeeded && authResult.Principal != null)
            {
                var user = authResult.Principal;
                var dsiIdentity = user?.Identity;

                // If no roles are specified, consider the user valid
                if (string.IsNullOrEmpty(roles) && dsiIdentity.IsAuthenticated)
                {
                    return true;
                }

                var validRoles = roles.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()).ToList();
                var userRoles = user.Claims
                    .Where(c => c.Type == ClaimConstants.RoleName)
                    .Select(c => c.Value)
                    .Distinct()
                    .ToList();

                // Check if the user has at least one of the valid roles
                foreach (var role in validRoles)
                {
                    if (userRoles.Contains(role))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<User> GetLoginUser()
        {
            var authResult = await _httpContext.AuthenticateAsync(ApplicationConstants.DSIClientCookieScheme);

            if (authResult.Succeeded && authResult.Principal != null)
            {
                var user = authResult.Principal;

                string userId = user.Claims.Where(c => c.Type.Contains(ClaimConstants.NameIdentifier)).Select(c => c.Value).SingleOrDefault();
                var userOrganization = JsonConvert.DeserializeObject<Organization>
                (
                    user.Claims.Where(c => c.Type == ClaimConstants.Organisation)
                    .Select(c => c.Value)
                    .FirstOrDefault()
                );

                var loginUser = new User()
                {
                    Id = new Guid(userId),
                    GivenName = user.Claims.Where(c => c.Type.Contains(ClaimConstants.FirstName)).Select(c => c.Value).SingleOrDefault(),
                    Surname = user.Claims.Where(c => c.Type.Contains(ClaimConstants.Surname)).Select(c => c.Value).SingleOrDefault(),
                    Email = user.Claims.Where(c => c.Type.Contains(ClaimConstants.EmailAddress)).Select(c => c.Value).SingleOrDefault(),
                    Organization = userOrganization,
                    Roles = user.Claims
                    .Where(c => c.Type == ClaimConstants.RoleName)
                    .Select(c => c.Value)
                    .Distinct()
                    .ToList()
                };

                return loginUser;
            }

            return null;
        }
    }
}