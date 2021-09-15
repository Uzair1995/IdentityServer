# Identity Server 4 

## Migrations Command
(1) Add-Migration InitialCreateConfigurationDbContext -c ConfigurationDbContext
(2) Add-Migration InitialCreateGrantDbContext -c CustomPersistedGrantDbContext

(3) Update-Database -context ConfigurationDbContext
(4) update-database -context CustomPersistedGrantDbContext
