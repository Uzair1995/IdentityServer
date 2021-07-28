using IdentityServer.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Repositories
{
    public static class RegisterRepositories
    {
        public static IIdentityServerBuilder AddIdentityContext(this IServiceCollection services, string connectionString)
        {
            return services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(RegisterRepositories).Namespace));
                })
                .AddOperationalStore<CustomPersistedGrantDbContext>(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(RegisterRepositories).Namespace));
                });
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient<IPersistedGrantsRepository, PersistedGrantsRepository>();
            return services;
        }
    }
}
