namespace OrchardCore.DSI.Core.Models
{
    public class ApiServiceResponce
    {
        public Guid UserId { get; set; }

        public Guid ServiceId { get; set; }

        public Guid OrganisationId { get; set; }

        public List<Role> Roles { get; set; }

        public List<Identifier> Identifiers { get; set; }

        public List<Status> Status { get; set; }
    }
}
