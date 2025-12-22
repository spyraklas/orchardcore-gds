using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace OrchardCore.DSI.Core.Extensions
{
    public static class SessionExtensions
    {
        // Default serializer settings (you can override per call)
        private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,   // safer default
            NullValueHandling = NullValueHandling.Include,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None
        };

        /// <summary>
        /// Serialize object to JSON (UTF8) and set in session under the key.
        /// </summary>
        public static void SetObject<T>(
            this ISession session,
            string key,
            T value,
            JsonSerializerSettings? settings = null)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (key == null) throw new ArgumentNullException(nameof(key));

            // If value is null, we remove the key to keep session clean
            if (value == null)
            {
                session.Remove(key);
                return;
            }

            var json = JsonConvert.SerializeObject(value, settings ?? DefaultSettings);
            var bytes = Encoding.UTF8.GetBytes(json);
            session.Set(key, bytes);
        }

        /// <summary>
        /// Try to get and deserialize an object from session. Returns false if not found or deserialization fails.
        /// </summary>
        public static bool TryGetObject<T>(
            this ISession session,
            string key,
            out T? value,
            JsonSerializerSettings? settings = null)
        {
            value = default;

            if (session == null) throw new ArgumentNullException(nameof(session));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (!session.TryGetValue(key, out var bytes) || bytes == null || bytes.Length == 0)
                return false;

            var json = Encoding.UTF8.GetString(bytes);

            try
            {
                value = JsonConvert.DeserializeObject<T>(json, settings ?? DefaultSettings);
                return value is not null || typeof(T).IsClass; // treat null class as success (stored null)
            }
            catch
            {
                // swallow or log depending on your logging strategy
                value = default;
                return false;
            }
        }

        /// <summary>
        /// Get deserialized object or a provided default value if missing/invalid.
        /// </summary>
        public static T? GetObjectOrDefault<T>(
            this ISession session,
            string key,
            T? defaultValue = default,
            JsonSerializerSettings? settings = null)
        {
            return session.TryGetObject<T>(key, out var value, settings) ? value : defaultValue;
        }

        /// <summary>
        /// Remove a key from session safely.
        /// </summary>
        public static void RemoveObject(this ISession session, string key)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (key == null) throw new ArgumentNullException(nameof(key));
            session.Remove(key);
        }
    }

}
