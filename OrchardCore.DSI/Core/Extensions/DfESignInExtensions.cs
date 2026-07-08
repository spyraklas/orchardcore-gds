using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using OrchardCore.DSI.Config;
using OrchardCore.DSI.Core.Constants;
using OrchardCore.DSI.Core.Models;
using OrchardCore.Environment.Shell;
using System.Net;
using System.Security.Claims;
using YesSql;

namespace OrchardCore.DSI.Core.Extensions
{
    public static class DfESignInExtensions
    {
        public static void AddDfESignInAuthentication(this IServiceCollection services, IDfESignInConfig dfeSignInConfiguration, ShellSettings shellSettings)
        {
            //1. Configure HttpClient for DfE Public API Client
            services.AddHttpClient<DfEPublicApiClientFactory>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    UseDefaultCredentials = false,
                    PreAuthenticate = true,
                    Proxy = string.IsNullOrWhiteSpace(dfeSignInConfiguration.APIServiceProxyUrl) ? new WebProxy() : new WebProxy(new Uri(dfeSignInConfiguration.APIServiceProxyUrl, UriKind.Absolute))
                });
            services.Configure<DfESignInPublicApiConfig>(config =>
            {
                config.ClientId = dfeSignInConfiguration.ClientId;
                config.ServiceSecret = dfeSignInConfiguration.APIServiceSecret;
                config.ServiceUrl = dfeSignInConfiguration.APIServiceUrl;
                config.ServiceAudience = dfeSignInConfiguration.APIServiceAudience;
                config.Cryptography = dfeSignInConfiguration.Cryptography;
            });


            //2. Add And Configure DfE Sign In Authentication
            var tenantName = shellSettings.Name;
            services.AddAuthentication(options =>
            {
                // Do NOT change OrchardCore defaults
                options.DefaultScheme = "OrchardCore"; // Keep admin login intact
            })
            .AddCookie(ApplicationConstants.DSIClientCookieScheme, options =>
            {
                options.Cookie.Name = dfeSignInConfiguration.CookieName;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(dfeSignInConfiguration.CookieExpiration);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = dfeSignInConfiguration.SlidingExpiration;
                options.Cookie.HttpOnly = true;
            })
            .AddOpenIdConnect(ApplicationConstants.DSIClientScheme, options =>
            {
                options.SignInScheme = ApplicationConstants.DSIClientCookieScheme;

                options.ClientId = dfeSignInConfiguration.ClientId;
                options.ClientSecret = dfeSignInConfiguration.ClientSecret;
                options.Authority = dfeSignInConfiguration.Authority;
                options.MetadataAddress = dfeSignInConfiguration.MetaDataUrl;
                options.CallbackPath = new PathString(dfeSignInConfiguration.CallbackUrl);
                options.SignedOutRedirectUri = new PathString(dfeSignInConfiguration.SignoutRedirectUrl);
                options.SignedOutCallbackPath = new PathString(dfeSignInConfiguration.SignoutCallbackUrl);
                options.ResponseType = OpenIdConnectResponseType.Code;

                options.Scope.Clear();
                foreach (string scope in dfeSignInConfiguration.Scopes)
                {
                    options.Scope.Add(scope);
                }

                options.GetClaimsFromUserInfoEndpoint = dfeSignInConfiguration.GetClaimsFromUserInfoEndpoint;
                options.SaveTokens = dfeSignInConfiguration.SaveTokens;

                options.Events = new OpenIdConnectEvents()
                {
                    OnTokenValidated = async (tokenValidatedContext) =>
                    {
                        //checked Principal Identity and if IsAuthenticated
                        var principal = tokenValidatedContext.Principal;
                        var identity = principal?.Identity as ClaimsIdentity;
                        if (identity is null || !identity.IsAuthenticated) return;

                        // API Call to get user roles
                        var claims = tokenValidatedContext.Principal.Claims;
                        var userOrganization = JsonConvert.DeserializeObject<Organization>
                        (
                            claims.Where(c => c.Type == ClaimConstants.Organisation)
                            .Select(c => c.Value)
                            .FirstOrDefault()
                        );
                        var clientFactory = tokenValidatedContext.HttpContext.RequestServices.GetRequiredService<DfEPublicApiClientFactory>();
                        DfEPublicApiClient dfeSignInClient = clientFactory.CreateDfESignInClient();
                        string userId = claims.Where(c => c.Type.Contains(ClaimConstants.NameIdentifier)).Select(c => c.Value).SingleOrDefault();
                        var apiServiceResponce = await dfeSignInClient.GetOrganisationUser(userId, userOrganization.Id.ToString());

                        //Add role claims
                        foreach (var role in apiServiceResponce.Roles)
                        {
                            //Ensure there is no dublicates
                            if (role.Status.Id.Equals(1) && identity.Claims.Where(c => c.Type == ClaimConstants.RoleName && c.Value == role.Name).FirstOrDefault() == null)
                            {
                                identity.AddClaim(new Claim(ClaimConstants.RoleCode, role.Code, ClaimTypes.Role, tokenValidatedContext.Options.ClientId));
                                identity.AddClaim(new Claim(ClaimConstants.RoleId, role.Id.ToString(), ClaimTypes.Role, tokenValidatedContext.Options.ClientId));
                                identity.AddClaim(new Claim(ClaimConstants.RoleName, role.Name, ClaimTypes.Role, tokenValidatedContext.Options.ClientId));
                                identity.AddClaim(new Claim(ClaimConstants.RoleNumericId, role.NumericId.ToString(), ClaimTypes.Role, tokenValidatedContext.Options.ClientId));

                            }
                        }
                    }
                };
            });

        }


        public static SignOutResult UserSignout(bool usingMockUser = false, string redirectURL = "")
        {
            if (usingMockUser)
            {
                return new SignOutResult(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = redirectURL });
            }
            else
            {
                return new SignOutResult(new[] { OpenIdConnectDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme }, new AuthenticationProperties { RedirectUri = redirectURL });
            }
        }
    }
}
