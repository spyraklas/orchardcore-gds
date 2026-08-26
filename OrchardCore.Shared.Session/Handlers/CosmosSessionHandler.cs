using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OrchardCore.Shared.Session.Config;
using Azure.Data.Tables;

using System.Net;
 
namespace OrchardCore.Shared.Session.Handlers
{
    internal class CosmosSessionHandler //: ISessionHandler, IExpirableSessionStore
    {
        //private readonly CosmosClient _client;
        //private readonly SessionSettings _settings;
        //private readonly Container _container;

        //public CosmosSessionHandler(IOptions<SessionSettings> settings)
        //{
        //    _settings = settings.Value;
        //    if (string.IsNullOrEmpty(_settings.CosmosConnectionString)) throw new ArgumentNullException(nameof(_settings.CosmosConnectionString));
        //    if (string.IsNullOrEmpty(_settings.CosmosDatabase)) throw new ArgumentNullException(nameof(_settings.CosmosDatabase));
        //    if (string.IsNullOrEmpty(_settings.CosmosContainer)) throw new ArgumentNullException(nameof(_settings.CosmosContainer));

        //    _client = new CosmosClient(_settings.CosmosConnectionString);
        //    var db = _client.CreateDatabaseIfNotExistsAsync(_settings.CosmosDatabase).GetAwaiter().GetResult();
        //    var containerResponse = db.Database.CreateContainerIfNotExistsAsync(new ContainerProperties(_settings.CosmosContainer, "/SessionId") { DefaultTimeToLive = null }).GetAwaiter().GetResult();
        //    _container = containerResponse.Container;
        //}

        //private string ItemId(string sessionId, string key) => $"{sessionId}:{key}";

        //public async Task SetAsync<T>(string sessionId, string key, T value)
        //{
        //    var item = new
        //    {
        //        id = ItemId(sessionId, key),
        //        SessionId = sessionId,
        //        Key = key,
        //        Value = JsonSerializer.Serialize(value),
        //        ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(Math.Max(1, _settings.ExpirationMinutes))
        //    };

        //    await _container.UpsertItemAsync(item, new PartitionKey(sessionId));
        //}

        //public async Task<T?> GetAsync<T>(string sessionId, string key)
        //{
        //    try
        //    {
        //        var response = await _container.ReadItemAsync<dynamic>(ItemId(sessionId, key), new PartitionKey(sessionId));
        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            string json = response.Resource.Value;
        //            return JsonSerializer.Deserialize<T?>(json);
        //        }
        //    }
        //    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        //    {
        //        return default;
        //    }

        //    return default;
        //}

        //public Task RemoveAsync(string sessionId, string key)
        //{
        //    return _container.DeleteItemAsync<dynamic>(ItemId(sessionId, key), new PartitionKey(sessionId));
        //}

        //public async Task ClearAsync(string sessionId)
        //{
        //    var query = new QueryDefinition("SELECT c.id FROM c WHERE c.SessionId = @sessionId")
        //        .WithParameter("@sessionId", sessionId);

        //    await foreach (var item in _container.GetItemQueryIterator<dynamic>(query))
        //    {
        //        foreach (var doc in item)
        //        {
        //            await _container.DeleteItemAsync<dynamic>((string)doc.id, new PartitionKey(sessionId));
        //        }
        //    }
        //}

        //public async Task CleanExpiredAsync()
        //{
        //    var query = new QueryDefinition("SELECT c.id, c.SessionId FROM c WHERE c.ExpiresAt < @now")
        //        .WithParameter("@now", DateTimeOffset.UtcNow);

        //    await foreach (var page in _container.GetItemQueryIterator<dynamic>(query))
        //    {
        //        foreach (var doc in page)
        //        {
        //            await _container.DeleteItemAsync<dynamic>((string)doc.id, new PartitionKey((string)doc.SessionId));
        //        }
        //    }
        //}
    }
}
