namespace OrchardCore.DSI.Core
{
    public interface ITokenEncoder
    {
        string Base64Encode(byte[] stringInput);
    }
}
