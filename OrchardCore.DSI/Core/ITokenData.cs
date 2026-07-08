namespace OrchardCore.DSI.Core
{
    public interface ITokenData
    {
        IDictionary<string, object> Header { get; set; }
        IDictionary<string, object> Payload { get; set; }
    }
}
