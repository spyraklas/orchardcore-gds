using System.Text.Json.Serialization;

namespace OrchardCore.DSI.Core.Models
{
    public class ServiceUsers
    {
        [JsonPropertyName("UPIN")]
        public string OrgRef { get; set; }
        public List<User> Users { get; set; }

    }
}
