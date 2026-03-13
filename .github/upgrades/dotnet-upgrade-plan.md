# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade Bibliotekssystem.csproj
4. Upgrade Bibliotekssystem.Test\Bibliotekssystemtest.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

No projects are excluded from this upgrade.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                           | Current Version | New Version | Description                                                        |
|:---------------------------------------|:---------------:|:-----------:|:-------------------------------------------------------------------|
| Microsoft.EntityFrameworkCore          | 9.0.14          | 10.0.5      | Recommended for .NET 10.0                                          |
| Microsoft.EntityFrameworkCore.Design   | 9.0.14          | 10.0.5      | Recommended for .NET 10.0                                          |
| Microsoft.EntityFrameworkCore.Sqlite   | 9.0.14; 9.*     | 10.0.5      | Recommended for .NET 10.0                                          |
| xunit                                  | 2.9.3           | 2.9.3       | Deprecated; only updated for security issues. v3 migration advised |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### Bibliotekssystem.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore should be updated from `9.0.14` to `10.0.5` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Design should be updated from `9.0.14` to `10.0.5` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Sqlite should be updated from `9.0.14` to `10.0.5` (*recommended for .NET 10.0*)
  - xunit `2.9.3` is deprecated; only updated for security issues, v3 migration advised (*no version change*)

#### Bibliotekssystem.Test\Bibliotekssystemtest.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore should be updated from `9.0.14` to `10.0.5` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Design should be updated from `9.0.14` to `10.0.5` (*recommended for .NET 10.0*)
  - Microsoft.EntityFrameworkCore.Sqlite should be updated from `9.*` to `10.0.5` (*recommended for .NET 10.0*)
  - xunit `2.9.3` is deprecated; only updated for security issues, v3 migration advised (*no version change*)
