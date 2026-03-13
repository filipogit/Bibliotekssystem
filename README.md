# Bibliotekssystem

Ett bibliotekssystem byggt med **Blazor Server** och **Entity Framework Core** i C# (.NET 10) för att hantera böcker, medlemmar och lån via ett webbaserat gränssnitt.

## Funktioner

- Registrera och hantera böcker med sökning och sortering
- Registrera och hantera medlemmar
- Låna ut och returnera böcker
- Visa lånehistorik per bok och medlem
- Markering av försenade lån
- Snabbstatistik på startsidan
- Formulärvalidering med `DataAnnotations`

## Teknisk stack

| Del | Teknologi |
|---|---|
| Språk | C# 13.0 |
| Framework | .NET 10 |
| UI | Blazor Server |
| ORM | Entity Framework Core 10.0.5 |
| Databas | SQLite (`library.db`) |
| Testramverk | xUnit |
| IDE | Visual Studio 2022 |

## Projektstruktur

```
Bibliotekssystem/
+-- Data/
|   +-- LibraryContext.cs          # EF Core DbContext
+-- Interfaces/
|   +-- ISearchable.cs             # Sökgränssnitt
+-- Migrations/                    # EF Core-migrationer
+-- Models/
|   +-- LibraryItem.cs             # Basklass
|   +-- Book.cs                    # Bokmodell
|   +-- Member.cs                  # Medlemsmodell
|   +-- Loan.cs                    # Lånemodell
+-- Repositories/
|   +-- BookRepository.cs          # Databasoperationer för böcker
+-- Bibliotekssystem.Web/
|   +-- Components/
|       +-- Layout/
|       |   +-- NavMenu.razor
|       +-- Pages/
|       |   +-- Home.razor         # Startsida med statistik (/)
|       |   +-- Books.razor        # Boklista (/books)
|       |   +-- BookDetail.razor   # Bokdetaljer (/books/{id})
|       |   +-- Members.razor      # Medlemslista (/members)
|       |   +-- MemberDetail.razor # Medlemsdetaljer (/members/{id})
|       |   +-- Loans.razor        # Utlåning (/loans)
|       +-- Shared/
|           +-- BookCard.razor     # Återanvändbar bokkomponent
|           +-- StatusBadge.razor  # Återanvändbar statusbadge
+-- Bibliotekssystem.Test/
    +-- BookTests.cs
    +-- BookRepositoryTests.cs
    +-- LoanTests.cs
    +-- SearchTests.cs
    +-- LibraryStatisticsTests.cs
```

## Databasmodell

```
LibraryItem (basklass)
+-- Id             int (PK)
+-- Title          string
+-- PublishedYear  int
+-- IsAvailable    bool
+-- BorrowedBy     string?

Book : LibraryItem
+-- ISBN           string (unik index)
+-- Author         string

Member
+-- Id             int (PK)
+-- Name           string
+-- Email          string
+-- MemberSince    DateTime

Loan
+-- Id             int (PK)
+-- BookId         int (FK -> Book)
+-- MemberId       int (FK -> Member)
+-- LoanDate       DateTime
+-- DueDate        DateTime
+-- ReturnDate     DateTime?
```

**Relationer:**
- En `Book` kan ha många `Loan`
- En `Member` kan ha många `Loan`
- Ett `Loan` tillhör exakt en `Book` och en `Member`

## Sidor i Blazor-gränssnittet

| Sida | Route | Beskrivning |
|---|---|---|
| Hem | `/` | Välkomstvy med snabbstatistik |
| Böcker | `/books` | Lista, sök, sortera, lägg till bok |
| Bokdetaljer | `/books/{id}` | Detaljvy, lånehistorik, lån/returnera |
| Medlemmar | `/members` | Lista, lägg till medlem |
| Medlemsdetaljer | `/members/{id}` | Detaljvy med lånehistorik |
| Utlåning | `/loans` | Skapa lån, lista aktiva lån |

## Krav

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 (rekommenderas) eller annan C#-IDE

## Installation

1. Klona projektet:
   ```bash
   git clone https://github.com/filipogit/Bibliotekssystem.git
   cd Bibliotekssystem
   ```

2. Skapa databasen med EF Core-migrationer:
   ```bash
   dotnet ef database update --project Bibliotekssystem.csproj
   ```

## Köra projektet

### Via Visual Studio
1. Öppna lösningen i Visual Studio 2022
2. Högerklicka på `Bibliotekssystem.Web` -> **Set as Startup Project**
3. Tryck `F5` eller klicka **Start**
4. Webbläsaren öppnas automatiskt på `https://localhost:{port}`

### Via kommandotolken
```bash
dotnet run --project Bibliotekssystem.Web
```

## Köra tester

### Via Visual Studio
1. Öppna **Test Explorer** via **View > Test Explorer**
2. Klicka **Run All**

### Via kommandotolken
```bash
dotnet test Bibliotekssystem.Test/Bibliotekssystemtest.csproj
```

Nuvarande testresultat: **27 tester, 0 fel**

## Kodstandard

Projektet följer moderna C#-konventioner:

- `nullable enable` för null-säkerhet
- `async/await` för alla databasoperationer
- `DataAnnotations` för modell- och formulärvalidering
- Arv och polymorfism (`LibraryItem` -> `Book`)
- Återanvändbara Blazor-komponenter i `Shared/`

## Författare

Utvecklat som ett skolprojekt för att demonstrera objektorienterad programmering och webbutveckling med C# och Blazor.
