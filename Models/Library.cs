using Bibliotekssystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Services
{
    public class Library
    {
        private List<LibraryItem> items;

        public Library()
        {
            items = new List<LibraryItem>();
        }

        // Lägg till objekt
        public void AddItem(LibraryItem item)
        {
            items.Add(item);
            Console.WriteLine($"Lade till: {item.Title}");
        }

        // ===== SÖKFUNKTIONER =====

        // Generell sökning (fungerar för alla typer)
        public List<LibraryItem> Search(string searchTerm)
        {
            return items.Where(item => item.Matches(searchTerm)).ToList();
        }

        // Sök specifikt efter böcker
        public List<Book> SearchBooks(string searchTerm)
        {
            return items.OfType<Book>()
                       .Where(book => book.Matches(searchTerm))
                       .ToList();
        }

        // Sök efter titel
        public List<LibraryItem> SearchByTitle(string title)
        {
            return items.Where(item => item.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                       .ToList();
        }

        // Sök efter författare (endast böcker)
        public List<Book> SearchByAuthor(string author)
        {
            return items.OfType<Book>()
                       .Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                       .ToList();
        }

        // Sök efter ISBN
        public Book? SearchByISBN(string isbn)
        {
            return items.OfType<Book>()
                       .FirstOrDefault(book => book.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }

        // ===== SORTERINGSFUNKTIONER =====

        // Sortera alfabetiskt efter titel
        public List<LibraryItem> SortByTitle(bool ascending = true)
        {
            return ascending
                ? items.OrderBy(item => item.Title).ToList()
                : items.OrderByDescending(item => item.Title).ToList();
        }

        // Sortera efter utgivningsår
        public List<LibraryItem> SortByYear(bool ascending = true)
        {
            return ascending
                ? items.OrderBy(item => item.PublishedYear).ToList()
                : items.OrderByDescending(item => item.PublishedYear).ToList();
        }

        // Sortera böcker efter författare
        public List<Book> SortBooksByAuthor(bool ascending = true)
        {
            var books = items.OfType<Book>();
            return ascending
                ? books.OrderBy(book => book.Author).ToList()
                : books.OrderByDescending(book => book.Author).ToList();
        }

        // ===== STATISTIKFUNKTIONER =====

        // Totalt antal objekt
        public int GetTotalItemCount()
        {
            return items.Count;
        }

        // Totalt antal böcker
        public int GetTotalBookCount()
        {
            return items.OfType<Book>().Count();
        }

        // Antal utlånade objekt
        public int GetBorrowedItemCount()
        {
            return items.Count(item => !item.IsAvailable);
        }

        // Antal utlånade böcker
        public int GetBorrowedBookCount()
        {
            return items.OfType<Book>().Count(book => !book.IsAvailable);
        }

        // Antal tillgängliga objekt
        public int GetAvailableItemCount()
        {
            return items.Count(item => item.IsAvailable);
        }

        // Mest aktiv låntagare
        public string? GetMostActiveBorrower()
        {
            var borrowerCounts = items
                .Where(item => item.BorrowedBy != null)
                .GroupBy(item => item.BorrowedBy)
                .Select(group => new { Borrower = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            return borrowerCounts?.Borrower;
        }

        // Detaljerad statistik per låntagare
        public Dictionary<string, int> GetBorrowerStatistics()
        {
            return items
                .Where(item => item.BorrowedBy != null)
                .GroupBy(item => item.BorrowedBy!)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        // Få alla utlånade böcker
        public List<LibraryItem> GetBorrowedItems()
        {
            return items.Where(item => !item.IsAvailable).ToList();
        }

        // Få alla tillgängliga böcker
        public List<LibraryItem> GetAvailableItems()
        {
            return items.Where(item => item.IsAvailable).ToList();
        }

        // Utskriftsmetoder
        public void ShowAllItems()
        {
            Console.WriteLine("\n=== ALLA OBJEKT I BIBLIOTEKET ===\n");
            foreach (var item in items)
            {
                Console.WriteLine(item.GetInfo());
            }
            Console.WriteLine($"\nTotalt: {items.Count} objekt");
        }

        public void ShowStatistics()
        {
            Console.WriteLine("\n=== BIBLIOTEKSSTATISTIK ===\n");
            Console.WriteLine($"Totalt antal objekt: {GetTotalItemCount()}");
            Console.WriteLine($"Totalt antal böcker: {GetTotalBookCount()}");
            Console.WriteLine($"Antal utlånade objekt: {GetBorrowedItemCount()}");
            Console.WriteLine($"Antal tillgängliga objekt: {GetAvailableItemCount()}");

            var mostActive = GetMostActiveBorrower();
            if (mostActive != null)
            {
                var stats = GetBorrowerStatistics();
                Console.WriteLine($"\nMest aktiv låntagare: {mostActive} ({stats[mostActive]} böcker)");

                Console.WriteLine("\nAlla låntagare:");
                foreach (var borrower in stats.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"  - {borrower.Key}: {borrower.Value} böcker");
                }
            }
            else
            {
                Console.WriteLine("\nInga aktiva låntagare för tillfället.");
            }
        }
    }
}