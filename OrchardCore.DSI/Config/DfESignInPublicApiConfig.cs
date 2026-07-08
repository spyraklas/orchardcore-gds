namespace OrchardCore.DSI.Config
{
    public class DfESignInPublicApiConfig : IDfESignInPublicApiConfig
    {
        public string ClientId { get; set; }
        public string ServiceSecret { get; set; }
        public string ServiceUrl { get; set; }
        public string Cryptography { get; set; }
        public string ServiceAudience { get; set; }
    }
}
