namespace OrchardCore.DSI.Core
{
    public class DfEPublicApiClient : IDfEPublicApiClient
    {
        public DfEPublicApiClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; private set; }

        public string ServiceId { get; set; }

        public string ServiceUrl { get; set; }
    }
}
