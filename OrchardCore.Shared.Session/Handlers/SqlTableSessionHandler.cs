using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using OrchardCore.Shared.Session.Config;
using System.Text.Json;

namespace OrchardCore.Shared.Session.Handlers
{
    internal class SqlTableSessionHandler : ISessionHandler, IExpirableSessionStore
    {
        private readonly string _connectionString;
        private readonly SessionSettings _settings;

        public SqlTableSessionHandler(IOptions<SessionSettings> settings)
        {
            _settings = settings.Value;
            _connectionString = _settings.SqlConnectionString ?? throw new ArgumentNullException(nameof(_settings.SqlConnectionString));

            EnsureTable();
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        private void EnsureTable()
        {
            using var conn = CreateConnection();
            conn.Execute(@"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Sessions' and xtype='U')
                            CREATE TABLE [dbo].[Sessions](
                                [SessionId] NVARCHAR(200) NOT NULL,
                                [Key] NVARCHAR(200) NOT NULL,
                                [Value] NVARCHAR(MAX) NULL,
                                [ExpiresAt] DATETIMEOFFSET NULL,
                                CONSTRAINT PK_Sessions PRIMARY KEY(SessionId, [Key])
                            );");
        }

        public async Task SetAsync<T>(string sessionId, string key, T value)
        {
            await CleanExpiredAsync();

            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(Math.Max(1, _settings.ExpirationMinutes));
            var json = JsonSerializer.Serialize(value);

            using var conn = CreateConnection();
            await conn.ExecuteAsync(@"MERGE INTO dbo.Sessions WITH (HOLDLOCK) AS target
USING (VALUES (@SessionId, @Key, @Value, @ExpiresAt)) AS source (SessionId, [Key], [Value], ExpiresAt)
ON (target.SessionId = source.SessionId AND target.[Key] = source.[Key])
WHEN MATCHED THEN UPDATE SET [Value] = source.[Value], ExpiresAt = source.ExpiresAt
WHEN NOT MATCHED THEN INSERT (SessionId, [Key], [Value], ExpiresAt) VALUES (source.SessionId, source.[Key], source.[Value], source.ExpiresAt);", new { SessionId = sessionId, Key = key, Value = json, ExpiresAt = expiresAt });
        }

        public async Task<T?> GetAsync<T>(string sessionId, string key)
        {
            await CleanExpiredAsync();

            using var conn = CreateConnection();
            var json = await conn.QuerySingleOrDefaultAsync<string?>("SELECT [Value] FROM dbo.Sessions WHERE SessionId = @SessionId AND [Key] = @Key", new { SessionId = sessionId, Key = key });
            if (json is null) return default;
            return JsonSerializer.Deserialize<T?>(json);
        }

        public async Task RemoveAsync(string sessionId, string key)
        {
            using var conn = CreateConnection();
            await conn.ExecuteAsync("DELETE FROM dbo.Sessions WHERE SessionId = @SessionId AND [Key] = @Key", new { SessionId = sessionId, Key = key });
        }

        public Task ClearAsync(string sessionId)
        {
            using var conn = CreateConnection();
            return conn.ExecuteAsync("DELETE FROM dbo.Sessions WHERE SessionId = @SessionId", new { SessionId = sessionId });
        }

        public Task CleanExpiredAsync()
        {
            using var conn = CreateConnection();
            return conn.ExecuteAsync("DELETE FROM dbo.Sessions WHERE ExpiresAt IS NOT NULL AND ExpiresAt < SYSUTCDATETIMEOFFSET()");
        }
    }
}
