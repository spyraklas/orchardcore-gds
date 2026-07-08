namespace OrchardCore.DSI.Core.Models
{
    public class ServiceUsersRequest
    {
        public List<string> OrgRefs { get; set; } = new List<string>();

        public List<string> Roles { get; set; } = new List<string>();
    }
}
