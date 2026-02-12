using Bibliotekssystem.Models;
using Bibliotekssystem.Services;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        // Lägg till böcker
        library.AddItem(new Book("978-91-1", "Harry Potter", "J.K. Rowling", 1997));
        library.AddItem(new Book("978-91-2", "1984", "George Orwell", 1949));
        library.AddItem(new Book("978-91-3", "Sagan om ringen", "J.R.R. Tolkien", 1954));
        library.AddItem(new Book("978-91-4", "Hobbit", "J.R.R. Tolkien", 1937));

        // Lägg till tidningar och DVD
        library.AddItem(new Magazine("M001", "National Geographic", 2026, 2, "NatGeo", "Feb"));
        library.AddItem(new DVD("D001", "Inception", 2010, "Christopher Nolan", 148, "Sci-Fi"));

        // Låna ut några böcker
        var items = library.Search("Harry Potter");
        items[0].CheckOut("Anna Andersson");

        items = library.Search("1984");
        items[0].CheckOut("Erik Eriksson");

        items = library.Search("Hobbit");
        items[0].CheckOut("Anna Andersson");

        // ===== DEMONSTRATION AV SÖKNING =====
        Console.WriteLine("\n=== SÖK EFTER 'TOLKIEN' ===");
        var tolkienBooks = library.SearchByAuthor("Tolkien");
        foreach (var book in tolkienBooks)
        {
            Console.WriteLine(book.GetInfo());
        }

        Console.WriteLine("\n=== SÖK EFTER 'RING' ===");
        var searchResults = library.Search("ring");
        foreach (var item in searchResults)
        {
            Console.WriteLine(item.GetInfo());
        }

        // ===== DEMONSTRATION AV SORTERING =====
        Console.WriteLine("\n=== SORTERA EFTER TITEL (A-Ö) ===");
        var sortedByTitle = library.SortByTitle();
        foreach (var item in sortedByTitle)
        {
            Console.WriteLine($"{item.Title} ({item.PublishedYear})");
        }

        Console.WriteLine("\n=== SORTERA EFTER ÅR (ÄLDST FÖRST) ===");
        var sortedByYear = library.SortByYear();
        foreach (var item in sortedByYear)
        {
            Console.WriteLine($"{item.Title} ({item.PublishedYear})");
        }

        // ===== STATISTIK =====
        library.ShowStatistics();
    }
}