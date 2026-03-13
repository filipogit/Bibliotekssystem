using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using Bibliotekssystem.Services;

class Program
{
    static void Main(string[] args)
    {
        using var context = new LibraryContext();
        context.Database.EnsureCreated();

        var library = new Library(context);

        SeedLibrary(library, context);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== BIBLIOTEKSSYSTEM - SÖKMENY ===");
            Console.WriteLine("1. Sök efter allt (titel, författare, ISBN, år)");
            Console.WriteLine("2. Sök efter titel");
            Console.WriteLine("3. Sök efter författare");
            Console.WriteLine("4. Sök efter ISBN");
            Console.WriteLine("5. Visa alla böcker");
            Console.WriteLine("0. Avsluta");
            Console.Write("\nVälj alternativ: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GeneralSearch(library);
                    break;
                case "2":
                    SearchByTitle(library);
                    break;
                case "3":
                    SearchByAuthor(library);
                    break;
                case "4":
                    SearchByISBN(library);
                    break;
                case "5":
                    library.ShowAllItems();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Avslutar programmet...");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        }
    }

    static void GeneralSearch(Library library)
    {
        Console.Write("\nAnge sökterm: ");
        string searchTerm = Console.ReadLine() ?? "";

        var results = library.Search(searchTerm);

        Console.WriteLine($"\n=== Hittade {results.Count} resultat ===\n");
        foreach (var item in results)
        {
            Console.WriteLine(item.GetInfo());
        }

        if (results.Count == 0)
        {
            Console.WriteLine("Inga resultat hittades.");
        }
    }

    static void SearchByTitle(Library library)
    {
        Console.Write("\nAnge titel: ");
        string title = Console.ReadLine() ?? "";

        var results = library.SearchByTitle(title);

        Console.WriteLine($"\n=== Hittade {results.Count} resultat ===\n");
        foreach (var item in results)
        {
            Console.WriteLine(item.GetInfo());
        }

        if (results.Count == 0)
        {
            Console.WriteLine("Inga böcker med den titeln hittades.");
        }
    }

    static void SearchByAuthor(Library library)
    {
        Console.Write("\nAnge författare: ");
        string author = Console.ReadLine() ?? "";

        var results = library.SearchByAuthor(author);

        Console.WriteLine($"\n=== Hittade {results.Count} böcker ===\n");
        foreach (var book in results)
        {
            Console.WriteLine(book.GetInfo());
        }

        if (results.Count == 0)
        {
            Console.WriteLine("Inga böcker av den författaren hittades.");
        }
    }

    static void SearchByISBN(Library library)
    {
        Console.Write("\nAnge ISBN: ");
        string isbn = Console.ReadLine() ?? "";

        var book = library.SearchByISBN(isbn);

        if (book != null)
        {
            Console.WriteLine("\n=== Bok hittad ===\n");
            Console.WriteLine(book.GetInfo());
        }
        else
        {
            Console.WriteLine("\nIngen bok med det ISBN-numret hittades.");
        }
    }

    static void SeedLibrary(Library library, LibraryContext context)
    {
        if (context.Books.Any())
            return;

        library.AddBook(new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954));
        library.AddBook(new Book("978-91-0-067891-2", "Harry Potter och de vises sten", "J.K. Rowling", 1997));
        library.AddBook(new Book("978-91-0-098765-4", "Hobbit", "J.R.R. Tolkien", 1937));
        library.AddBook(new Book("978-91-0-055555-5", "1984", "George Orwell", 1949));
    }
}