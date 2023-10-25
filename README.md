# CleanArchitecture.NET

# Command for db migration
- ```bash
  dotnet ef migrations add "InitialMigration" --project Infrastructure --startup-project CleanArchitecture --output-dir Persistence\Migrations
- ```bash
  dotnet ef database update --project Infrastructure --startup-project CleanArchitecture
