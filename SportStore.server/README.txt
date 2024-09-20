
#Команда для накатывания МИГРАЦИИ в контексте IdentityApplicationDbContext

	dotnet ef migrations add InitialIdentity -c IdentityApplicationDbContext

-------------------------------------------------------------------------
#Команда для накатывания МИГРАЦИИ в контексте ApplicationDbContext

	dotnet ef migrations add InitialApplicationData -c ApplicationDbContext
-------------------------------------------------------------------------

#Команда для ОБНОВЛЕНИЯ базы данных после миграции в контексте IdentityApplicationDbContext

	dotnet ef database update -c IdentityApplicationDbContext

-------------------------------------------------------------------------
#Команда для ОБНОВЛЕНИЯ базы данных после миграции в контексте ApplicationDbContext

	dotnet ef database update -c ApplicationDbContext
-------------------------------------------------------------------------