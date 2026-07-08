using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OrchardCore.DSI.Config;
using OrchardCore.DSI.Core.Models;
using System.Net.Http.Json;
using System.Text;
using System.Web;


namespace OrchardCore.DSI.Core.Extensions
{
    public static class DfEPublicApiClientExtensions
    {
        public static void ConfigureDfeSignInPublicApiClient(this IServiceCollection services, IConfigurationSection dfeSignInPublicApiConfig)
        {
            services.Configure<DfESignInPublicApiConfig>(dfeSignInPublicApiConfig);
            services.AddHttpClient<IDfEPublicApiClientFactory, DfEPublicApiClientFactory>();
        }

        /// <summary>
        /// Retrieves organization users by organization reference and roles.
        /// </summary>
        /// <param name="dfeSignInClient">Configuration for DfE Sign-In authentication and API settings.</param>
        /// <param name="organisationUsersRequest">Request object with organization reference and roles for filtering users.</param>
        /// <returns>List of organization users matching the specified reference and roles.</returns>
        public static async Task<OrganisationUsersResponse> GetOrganisationUsersByRoles(this DfEPublicApiClient dfeSignInClient, OrganisationUsersRequest organisationUsersRequest)
        {
            var queryUrl = new StringBuilder($"{dfeSignInClient.ServiceUrl}/organisations/{organisationUsersRequest.OrgRef}/users");

            if (organisationUsersRequest.OrgRoles?.Count > 0)
            {
                queryUrl.Append($"?roles={string.Join(',', organisationUsersRequest.OrgRoles)}");
            }

            var result = await dfeSignInClient.HttpClient.GetAsync(new Uri(queryUrl.ToString()));

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<OrganisationUsersResponse>();
            }
            else
            {
                return new OrganisationUsersResponse()
                {
                    Users = new List<User>()
                };
            }
        }

        /// <summary>
        /// Retrieves organization users by organizationId and userId.
        /// </summary>
        /// <param name="dfeSignInClient">Configuration for DfE Sign-In authentication and API settings.</param>
        /// <param name="userId">User ID for filtering organization users.</param>
        /// <param name="organizationId">Organization ID for filtering organization users.</param>
        /// <returns>List of organization users matching the specified organizationId and userId.</returns>
        /// <exception cref="MemberAccessException">Thrown if required members (ServiceUrl, ServiceId, organizationId, or userId) are not set.</exception>

        public static async Task<ApiServiceResponce> GetOrganisationUser(this DfEPublicApiClient dfeSignInClient, string userId = "", string organizationId = "")
        {

            if (string.IsNullOrEmpty(dfeSignInClient.ServiceUrl) | string.IsNullOrEmpty(dfeSignInClient.ServiceId) | string.IsNullOrEmpty(organizationId) | string.IsNullOrEmpty(userId))
            {
                throw new MemberAccessException("Required Member(s) not set");
            }

            var queryUrl = new StringBuilder($"{dfeSignInClient.ServiceUrl}/services/{dfeSignInClient.ServiceId}/organisations/{organizationId}/users/{userId}");

            var result = await dfeSignInClient.HttpClient.GetAsync(new Uri(queryUrl.ToString()));

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ApiServiceResponce>();
            }
            else
            {
                return new ApiServiceResponce()
                {
                    Roles = new List<Role>()
                };
            }
        }

        /// <summary>
        /// Retrieves external user details by organization reference and email.
        /// </summary>
        /// <param name="dfeSignInClient">Configuration for DfE Sign-In authentication and API settings.</param>
        /// <param name="organisationUsersRequest">Request with organization reference and email for filtering users.</param>
        /// <returns>List of external users matching the organization reference and email.</returns>
        /// <exception cref="ArgumentNullException">Thrown if organisationUsersRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown if organisationUsersRequest.EmailId is null.</exception>
        public static async Task<OrganisationUsersResponse> GetExternalUserDetailsByEmail(this DfEPublicApiClient dfeSignInClient, OrganisationUsersRequest organisationUsersRequest)
        {
            if (organisationUsersRequest == null)
                throw new ArgumentNullException(nameof(organisationUsersRequest));

            if (string.IsNullOrEmpty(organisationUsersRequest.EmailId))
                throw new ArgumentException("EmailId cannot be null or empty", nameof(organisationUsersRequest));

            organisationUsersRequest.OrgRef = Constants.ApplicationConstants.ExternalAuditorUpin;
            var queryUrl = new StringBuilder($"{dfeSignInClient.ServiceUrl}/organisations/{organisationUsersRequest.OrgRef}/users?email={HttpUtility.UrlEncode(organisationUsersRequest.EmailId)}");

            var result = await dfeSignInClient.HttpClient.GetAsync(new Uri(queryUrl.ToString()));
            if (result.IsSuccessStatusCode)
            {
                var jsonResult = await result.Content.ReadFromJsonAsync<OrganisationUsersResponse>();
                if (!organisationUsersRequest.OrgRoles.IsNullOrEmpty() && organisationUsersRequest.OrgRoles.Any())
                {
                    jsonResult.Users = jsonResult.Users.Where(x => x.Roles.Intersect(organisationUsersRequest.OrgRoles, StringComparer.OrdinalIgnoreCase).Any()).ToList();
                }
                return jsonResult;
            }
            else
            {
                return new OrganisationUsersResponse()
                {
                    Users = new List<User>()
                };
            }
        }

        /// <summary>
        /// Retrieves user details by organization reference and email.
        /// </summary>
        /// <param name="dfeSignInClient">Configuration for DfE Sign-In authentication and API settings.</param>
        /// <param name="organisationUsersRequest">Request containing organization reference and email for filtering users.</param>
        /// <returns>List of external users matching the organization reference and email.</returns>
        /// <exception cref="ArgumentNullException">Thrown if organisationUsersRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown if organisationUsersRequest.OrgRef or EmailId is null.</exception>
        public static async Task<OrganisationUsersResponse> GetUserDetailsByEmail(this DfEPublicApiClient dfeSignInClient, OrganisationUsersRequest organisationUsersRequest)
        {
            if (organisationUsersRequest == null)
                throw new ArgumentNullException(nameof(organisationUsersRequest));

            if (string.IsNullOrEmpty(organisationUsersRequest.OrgRef))
                throw new ArgumentException("OrgRef cannot be null or empty", nameof(organisationUsersRequest));

            if (string.IsNullOrEmpty(organisationUsersRequest.EmailId))
                throw new ArgumentException("EmailId cannot be null or empty", nameof(organisationUsersRequest));

            var queryUrl = new StringBuilder($"{dfeSignInClient.ServiceUrl}/organisations/{organisationUsersRequest.OrgRef}/users?email={HttpUtility.UrlEncode(organisationUsersRequest.EmailId)}");

            var result = await dfeSignInClient.HttpClient.GetAsync(new Uri(queryUrl.ToString()));
            if (result.IsSuccessStatusCode)
            {
                var jsonResult = await result.Content.ReadFromJsonAsync<OrganisationUsersResponse>();
                if (!organisationUsersRequest.OrgRoles.IsNullOrEmpty() && organisationUsersRequest.OrgRoles.Any())
                {
                    jsonResult.Users = jsonResult.Users.Where(x => x.Roles.Intersect(organisationUsersRequest.OrgRoles, StringComparer.OrdinalIgnoreCase).Any()).ToList();
                }
                return jsonResult;
            }
            else
            {
                return new OrganisationUsersResponse()
                {
                    Users = new List<User>()
                };
            }
        }

        /// <summary>
        /// Retrieves service users by OrgRefs and roles.
        /// </summary>
        /// <param name="dfeSignInClient"> Configuration for DfE Sign-In service authentication and API settings. </param>
        /// <param name="serviceUserRequest"> Request object with OrgRefs and roles for filtering. </param>
        /// <returns> List of service users matching the OrgRefs and roles. </returns>
        public static async Task<ServiceUsersResponse> GetServiceUsersByRoles(this DfEPublicApiClient dfeSignInClient, ServiceUsersRequest serviceUserRequest)
        {
            serviceUserRequest.OrgRefs = serviceUserRequest.OrgRefs.ConvertAll(s => s.Trim());
            serviceUserRequest.Roles = serviceUserRequest.Roles.ConvertAll(s => s.Trim().ToUpper());

            var results = new List<ServiceUsers>();

            if (!serviceUserRequest.Roles.Any() || serviceUserRequest.Roles.All(role => string.IsNullOrEmpty(role)) || !serviceUserRequest.OrgRefs.Any() || serviceUserRequest.OrgRefs.All(orgRef => string.IsNullOrEmpty(orgRef)))
            {
                return new ServiceUsersResponse()
                {
                    ServiceUsers = results
                };
            }

            foreach (var orgRef in serviceUserRequest.OrgRefs)
            {
                var queryUrl = new StringBuilder($"{dfeSignInClient.ServiceUrl}/organisations/{orgRef}/users");
                queryUrl.Append($"?roles={string.Join(',', serviceUserRequest.Roles)}");

                var response = await dfeSignInClient.HttpClient.GetAsync(new Uri(queryUrl.ToString()));

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ServiceUsers>();
                    apiResponse.Users = apiResponse.Users?.Where(u => u.UserStatus == 1).ToList();

                    if (apiResponse.Users != null && apiResponse.Users.Any())
                        results.Add(apiResponse);
                }
            }

            var serviceUsersResponse = new ServiceUsersResponse
            {
                ServiceUsers = results
            };

            return serviceUsersResponse;

        }

        public static bool IsNullOrEmpty(this List<string> list)
        {             
            return list == null || list.Count == 0; 
        }
    }
}
