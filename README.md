﻿# CleanArchitecture.NET

# Command for db migration
- dotnet ef migrations add "InitialMigration" --project Infrastructure --startup-project CleanArchitecture --output-dir Persistence\Migrations
- dotnet ef database update --project Infrastructure --startup-project CleanArchitecture
