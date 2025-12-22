namespace OrchardCore.DSI.Core
{
    public interface IDfEPublicApiClient
    {
        public HttpClient HttpClient { get; }

        public string ServiceId { get; set; }

        public string ServiceUrl { get; set; }
    }
}
