namespace OrchardCore.DSI.Core.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int NumericId { get; set; }
        public Status Status { get; set; }
    }
}
