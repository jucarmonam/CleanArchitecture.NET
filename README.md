# CleanArchitecture.NET

# Command for db migration
- ```bash
  dotnet ef migrations add "InitialMigration" --project src\Infrastructure --startup-project src\WebUI --output-dir Persistence\Migrations
- ```bash
  dotnet ef database update --project src\Infrastructure --startup-project src\WebUI
