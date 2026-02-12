# Bibliotekssystem

Ett konsolbaserat bibliotekssystem utvecklat i C# (.NET 9) för att hantera böcker, medlemmar och lån.

## Beskrivning

Detta är ett enkelt men funktionellt bibliotekssystem som gör det möjligt att:
- Registrera och hantera böcker
- Registrera och hantera medlemmar
- Hantera utlåning och återlämning av böcker
- Söka efter böcker
- Visa statistik över biblioteksverksamheten

## Funktioner

### Böcker (Book)
- ISBN, titel, författare, utgivningsår
- Tillgänglighetsstatus
- Formaterad bokinformation

### Medlemmar (Member)
- Medlems-ID, namn, e-post
- Medlemskap sedan datum
- Hantering av lånade böcker

### Lån (Loan)
- Koppling mellan bok och medlem
- Lånedatum, förfallodatum och återlämningsdatum
- Automatisk kontroll av försenade lån
- Status på återlämning

### Biblioteksstatistik (LibraryStatistics)
- Totalt antal böcker och tillgängliga böcker
- Aktiva lån och försenade lån
- Populäraste böcker

## Projektstruktur

## Krav

- .NET 9 SDK eller senare
- Visual Studio 2022 (rekommenderat) eller annan C#-IDE

## Installation

1. Klona projektet från GitHub:
2. Öppna lösningen i Visual Studio:
Eller öppna direkt i Visual Studio via **File > Open > Project/Solution**

## Hur man kör programmet

### Via Visual Studio
1. Öppna lösningen i Visual Studio 2022
2. Sätt `Bibliotekssystem` som startprojekt (högerklicka på projektet > **Set as Startup Project**)
3. Tryck på `F5` eller klicka på **Start** för att köra programmet

### Via kommandotolken

## Hur man kör tester

### Via Visual Studio
1. Öppna **Test Explorer** (__View > Test Explorer__)
2. Klicka på **Run All** för att köra alla tester

### Via kommandotolken

## Användning

Efter att programmet startat kan du:
1. Lägga till böcker i systemet
2. Registrera nya medlemmar
3. Låna ut böcker till medlemmar
4. Återlämna böcker
5. Söka efter böcker
6. Visa biblioteksstatistik

## Kodstandard

Projektet följer moderna C#-konventioner:
- Inkapsling med `init`-accessors där lämpligt
- Nullable reference types för bättre null-säkerhet
- Expression-bodied members för beräknade properties
- Immutable design patterns där möjligt

## Teknisk stack

- **Språk:** C# 13.0
- **Framework:** .NET 9
- **Testramverk:** xUnit / NUnit / MSTest
- **IDE:** Visual Studio 2022

## Författare

Utvecklat som ett skolprojekt för att demonstrera objektorienterad programmering i C#.

## Licens

Detta projekt är skapat för utbildningssyfte.