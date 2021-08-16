using IdentityServer.Repositories.Context;
using IdentityServer.Repositories.Interfaces;
using IdentityServer.Repositories.Models;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer.Repositories.Implementations
{
    class ClientDataRepository : IClientDataRepository
    {
        private readonly CustomPersistedGrantDbContext customPersistedGrantDbContext;

        public ClientDataRepository(CustomPersistedGrantDbContext customPersistedGrantDbContext)
        {
            this.customPersistedGrantDbContext = customPersistedGrantDbContext;
        }

        public async Task InsertOrUpdateClientDataAsync(ClientData clientData)
        {
            var oldData = await customPersistedGrantDbContext.ClientData.FindAsync(clientData.ClientId);
            if (oldData == null)
                customPersistedGrantDbContext.ClientData.Add(clientData);
            else
            {
                foreach (PropertyInfo info in clientData.GetType().GetProperties())
                {
                    info.SetValue(oldData, info.GetValue(clientData));
                }
                customPersistedGrantDbContext.ClientData.Update(oldData);
            }

            await customPersistedGrantDbContext.SaveChangesAsync();
        }

        public async Task<ClientData> GetClientDataUsingId(string clientId)
        {
            return await customPersistedGrantDbContext.ClientData.FindAsync(clientId);
        }
    }
}
