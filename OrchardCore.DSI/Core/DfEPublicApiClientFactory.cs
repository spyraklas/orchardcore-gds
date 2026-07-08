using OrchardCore.DSI.Config;
using OrchardCore.DSI.Core.Constants;
using OrchardCore.DSI.Core.Extensions;
using Microsoft.Extensions.Options;

namespace OrchardCore.DSI.Core
{
    public class DfEPublicApiClientFactory : IDfEPublicApiClientFactory
    {
        private readonly DfESignInPublicApiConfig _dfEPublicApiConfig;
        private readonly HttpClient _httpClient;

        public DfEPublicApiClientFactory(IOptions<DfESignInPublicApiConfig> dfEPublicApiConfig, HttpClient httpClient)
        {
            _dfEPublicApiConfig = dfEPublicApiConfig.Value;
            _httpClient = httpClient;
        }


        public DfEPublicApiClient CreateDfESignInClient()
        {
            var dfeSignInClient = new DfEPublicApiClient(_httpClient)
            {
                ServiceId = _dfEPublicApiConfig.ClientId,
                ServiceUrl = _dfEPublicApiConfig.ServiceUrl
            };

            var tokenData = new TokenData();
            tokenData.Header.Add(HeaderConstants.Type, "JWT");
            var token = new TokenBuilder(new TokenDataSerializer(), tokenData, new TokenEncoder(), new JsonWebAlgorithm())
                .UseAlgorithm(_dfEPublicApiConfig.Cryptography)
                .ForAudience(_dfEPublicApiConfig.ServiceAudience)
                .WithSecretKey(_dfEPublicApiConfig.ServiceSecret)
                .Issuer(_dfEPublicApiConfig.ClientId)
                .CreateToken();

            dfeSignInClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return dfeSignInClient;
        }
    }
}
