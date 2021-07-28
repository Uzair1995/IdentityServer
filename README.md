# Identity Server 4 

## Migrations
Add-Migration InitialCreateConfigurationDbContext -c ConfigurationDbContext
Add-Migration InitialCreateGrantDbContext -c PersistedGrantDbContext

Update-Database -c ConfigurationDbContext
Update-Database -c PersistedGrantDbContext