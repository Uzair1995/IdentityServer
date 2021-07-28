using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IdentityServer.Repositories.Models;

namespace IdentityServer.Repositories.Context
{
    public class CustomPersistedGrantDbContext : DbContext, IPersistedGrantDbContext
    {
        public CustomPersistedGrantDbContext(DbContextOptions<CustomPersistedGrantDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersistedGrant>().HasKey(x => x.Key);
            modelBuilder.Entity<DeviceFlowCodes>().HasNoKey();
            //modelBuilder.Entity<ClientData>().HasIndex(x => x.Email).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ClientData> ClientData { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
