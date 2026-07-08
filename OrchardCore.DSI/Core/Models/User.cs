namespace OrchardCore.DSI.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public int UserStatus { get; set; }

        public List<string> Roles { get; set; }

        public Organization Organization { get; set; }

    }
}
