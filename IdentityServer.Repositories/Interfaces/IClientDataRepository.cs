using IdentityServer.Repositories.Models;
using System.Threading.Tasks;

namespace IdentityServer.Repositories.Interfaces
{
    public interface IClientDataRepository
    {
        Task InsertOrUpdateClientDataAsync(ClientData clientData);
        Task<ClientData> GetClientDataUsingId(string clientId);
    }
}
