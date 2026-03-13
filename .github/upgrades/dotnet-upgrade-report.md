# .NET 10.0 Upgrade Report

## Project target framework modifications

| Project name                                        | Old Target Framework | New Target Framework | Commits                |
|:----------------------------------------------------|:--------------------:|:--------------------:|:-----------------------|
| Bibliotekssystem.csproj                             | net9.0               | net10.0              | d4390709, 33db78f3     |
| Bibliotekssystem.Test\Bibliotekssystemtest.csproj   | net9.0               | net10.0              | 0e4c9886, 8df129d1     |

## NuGet Packages

| Package Name                           | Old Version | New Version | Commit Id              |
|:---------------------------------------|:-----------:|:-----------:|:-----------------------|
| Microsoft.EntityFrameworkCore          | 9.0.14      | 10.0.5      | d4390709, 8df129d1     |
| Microsoft.EntityFrameworkCore.Design   | 9.0.14      | 10.0.5      | d4390709, 8df129d1     |
| Microsoft.EntityFrameworkCore.Sqlite   | 9.0.14      | 10.0.5      | d4390709, 8df129d1     |

## All commits

| Commit ID  | Description                                                        |
|:-----------|:-------------------------------------------------------------------|
| 9f5e13f8   | Commit upgrade plan                                                |
| d4390709   | Update Bibliotekssystem.csproj: EF Core Sqlite version bump        |
| 33db78f3   | Store final changes for step 'Upgrade Bibliotekssystem.csproj'     |
| 0e4c9886   | Update target framework in Bibliotekssystemtest.csproj             |
| 8df129d1   | Update Bibliotekssystemtest.csproj EF Core packages to v10.0.5     |

## Project feature upgrades

### Bibliotekssystem.csproj

- Target framework updated from `net9.0` to `net10.0`
- Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design, and Microsoft.EntityFrameworkCore.Sqlite packages updated from `9.0.14` to `10.0.5`

### Bibliotekssystem.Test\Bibliotekssystemtest.csproj

- Target framework updated from `net9.0` to `net10.0`
- Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design, and Microsoft.EntityFrameworkCore.Sqlite packages updated from `9.x` to `10.0.5`

## Next steps

- Consider migrating from xunit v2 (`2.9.3`) to xunit v3, as v2 is deprecated and only receives security updates. See: https://xunit.net/docs/getting-started/v3/migration
