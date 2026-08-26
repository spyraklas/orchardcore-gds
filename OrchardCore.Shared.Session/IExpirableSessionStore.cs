using System.Threading.Tasks;

namespace OrchardCore.Shared.Session.Handlers
{
    internal interface IExpirableSessionStore
    {
        Task CleanExpiredAsync();
    }
}
