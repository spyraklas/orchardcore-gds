namespace OrchardCore.Shared.Session.Config
{
    public class SessionSettings
    {
        public string Provider { get; set; } = "Memory";

        public string CookieName { get; set; } = "session-cookie";

        public int ExpirationMinutes { get; set; } = 30;

        public string? RedisConnectionString { get; set; }

        public string? SqlConnectionString { get; set; }

        public string? CosmosConnectionString { get; set; }

        public string? CosmosDatabase { get; set; }

        public string? CosmosContainer { get; set; }
    }
}
