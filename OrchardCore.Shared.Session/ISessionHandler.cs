namespace OrchardCore.Shared.Session
{
    public interface ISessionHandler
    {
        Task SetAsync<T>(string sessionId, string key, T value);

        Task<T?> GetAsync<T>(string sessionId, string key);

        Task RemoveAsync(string sessionId, string key);

        Task ClearAsync(string sessionId);
    }
}
