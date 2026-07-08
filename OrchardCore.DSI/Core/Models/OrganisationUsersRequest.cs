namespace OrchardCore.DSI.Core.Models
{
    public class OrganisationUsersRequest
    {
        public string EmailId { get; set; }
        public string OrgRef { get; set; }
        public string CollectionName { get; set; }
        public List<string> OrgRoles { get; set; } = new List<string>();
    }
}
