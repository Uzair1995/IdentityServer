using IdentityServer.Repositories;
using IdentityServer.Repositories.Context;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServerApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddIdentityContext(configuration["IdentityConnectionString"]);
            services.AddRepositories();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(x =>
                {
                    var authority = configuration["Identity:Authority"];
                    x.Authority = authority;
                    x.RequireHttpsMetadata = authority.StartsWith("https");
                    x.ApiName = configuration["Identity:ApiName"];
                }).AddIdentityServerAuthentication("bearerForAnyone", x =>
                {
                    var authority = configuration["Identity:Authority"];
                    x.Authority = authority;
                    x.RequireHttpsMetadata = authority.StartsWith("https");
                });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("IdentityServerAdmin", s =>
                {
                    s.RequireScope(
                        Configs.IdentityServer.ApiScopes
                        .Select(x => x.Name)
                        .Where(x => x.Contains("admin", System.StringComparison.InvariantCultureIgnoreCase))
                        .ToArray());
                });
                x.AddPolicy("AllAdmins", s =>
                {
                    s.RequireScope(
                        Configs.Config.CompileApiScopesFromRegisteredResources()
                        .Select(x => x.Name)
                        .Where(x => x.Contains("admin", System.StringComparison.InvariantCultureIgnoreCase))
                        .ToArray());
                });
                x.AddPolicy("AllSupports", s =>
                {
                    s.RequireScope(
                        Configs.Config.CompileApiScopesFromRegisteredResources()
                        .Select(x => x.Name)
                        .Where(x => x.Contains("support", System.StringComparison.InvariantCultureIgnoreCase))
                        .ToArray());
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Account Opening API Identity Server", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account Opening API Identity Server");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var configureContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<CustomPersistedGrantDbContext>();
                configureContext.Database.Migrate();
                persistedGrantDbContext.Database.Migrate();


                if (!configureContext.IdentityResources.Any())
                {
                    foreach (var resource in Configs.Config.IdentityResources)
                        configureContext.IdentityResources.Add(resource.ToEntity());
                    configureContext.SaveChanges();
                }
                if (!configureContext.ApiScopes.Any())
                {
                    foreach (var resource in Configs.Config.CompileApiScopesFromRegisteredResources())
                        configureContext.ApiScopes.Add(resource.ToEntity());
                    configureContext.SaveChanges();
                }
                if (!configureContext.ApiResources.Any())
                {
                    foreach (var resource in Configs.Config.CompileApiResourcesFromRegisteredResources())
                        configureContext.ApiResources.Add(resource.ToEntity());
                    configureContext.SaveChanges();
                }
                if (!configureContext.Clients.Any())
                {
                    foreach (var client in Configs.Config.CompileClientsInfoFromRegisteredResources())
                        configureContext.Clients.Add(client);
                    configureContext.SaveChanges();
                }
                if (!persistedGrantDbContext.ClientData.Any())
                {
                    foreach (var client in Configs.Config.CompileClientsDataFromRegisteredResources())
                        persistedGrantDbContext.ClientData.Add(client);
                    persistedGrantDbContext.SaveChanges();
                }
            }
        }
    }
}
