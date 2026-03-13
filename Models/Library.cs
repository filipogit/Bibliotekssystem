using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Services
{
    public class Library
    {
        private readonly LibraryContext _context;

        public Library(LibraryContext context)
        {
            _context = context;
        }

        // Lägg till objekt
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            Console.WriteLine($"Lade till: {book.Title}");
        }

        public void AddMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
            Console.WriteLine($"Lade till medlem: {member.Name}");
        }

        // ===== SÖKFUNKTIONER =====

        // Generell sökning
        public List<Book> Search(string searchTerm)
        {
            var term = searchTerm.ToLower();
            return _context.Books
                .Where(b => b.Title.ToLower().Contains(term) ||
                            b.Author.ToLower().Contains(term) ||
                            b.ISBN.ToLower().Contains(term) ||
                            b.PublishedYear.ToString().Contains(term))
                .ToList();
        }

        // Sök efter titel
        public List<Book> SearchByTitle(string title)
        {
            return _context.Books
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToList();
        }

        // Sök efter författare
        public List<Book> SearchByAuthor(string author)
        {
            return _context.Books
                .Where(b => b.Author.ToLower().Contains(author.ToLower()))
                .ToList();
        }

        // Sök efter ISBN
        public Book? SearchByISBN(string isbn)
        {
            return _context.Books
                .FirstOrDefault(b => b.ISBN.ToLower() == isbn.ToLower());
        }

        // ===== SORTERINGSFUNKTIONER =====

        public List<Book> SortByTitle(bool ascending = true)
        {
            return ascending
                ? _context.Books.OrderBy(b => b.Title).ToList()
                : _context.Books.OrderByDescending(b => b.Title).ToList();
        }

        public List<Book> SortByYear(bool ascending = true)
        {
            return ascending
                ? _context.Books.OrderBy(b => b.PublishedYear).ToList()
                : _context.Books.OrderByDescending(b => b.PublishedYear).ToList();
        }

        public List<Book> SortBooksByAuthor(bool ascending = true)
        {
            return ascending
                ? _context.Books.OrderBy(b => b.Author).ToList()
                : _context.Books.OrderByDescending(b => b.Author).ToList();
        }

        // ===== LÅN =====

        public void BorrowBook(int bookId, int memberId)
        {
            var book = _context.Books.Find(bookId);
            var member = _context.Members.Find(memberId);

            if (book == null || member == null)
            {
                Console.WriteLine("Bok eller medlem hittades inte.");
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine($"{book.Title} är redan utlånad.");
                return;
            }

            var loan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            book.IsAvailable = false;
            book.BorrowedBy = member.Name;

            _context.Loans.Add(loan);
            _context.SaveChanges();
            Console.WriteLine($"{book.Title} har lånats ut till {member.Name}.");
        }

        public void ReturnBook(int bookId)
        {
            var loan = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefault(l => l.BookId == bookId && l.ReturnDate == null);

            if (loan == null)
            {
                Console.WriteLine("Inget aktivt lån hittades för denna bok.");
                return;
            }

            loan.ReturnDate = DateTime.Now;
            loan.Book.IsAvailable = true;
            loan.Book.BorrowedBy = null;

            _context.SaveChanges();
            Console.WriteLine($"{loan.Book.Title} har returnerats av {loan.Member.Name}.");
        }

        // ===== STATISTIKFUNKTIONER =====

        public int GetTotalBookCount()
        {
            return _context.Books.Count();
        }

        public int GetBorrowedItemCount()
        {
            return _context.Books.Count(b => !b.IsAvailable);
        }

        public int GetBorrowedBookCount()
        {
            return _context.Books.Count(b => !b.IsAvailable);
        }

        public int GetAvailableItemCount()
        {
            return _context.Books.Count(b => b.IsAvailable);
        }

        public string? GetMostActiveBorrower()
        {
            var borrower = _context.Loans
                .GroupBy(l => l.Member.Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            return borrower?.Name;
        }

        public Dictionary<string, int> GetBorrowerStatistics()
        {
            return _context.Loans
                .GroupBy(l => l.Member.Name)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public List<Book> GetBorrowedItems()
        {
            return _context.Books.Where(b => !b.IsAvailable).ToList();
        }

        public List<Book> GetAvailableItems()
        {
            return _context.Books.Where(b => b.IsAvailable).ToList();
        }

        // Utskriftsmetoder
        public void ShowAllItems()
        {
            var books = _context.Books.ToList();
            Console.WriteLine("\n=== ALLA BÖCKER I BIBLIOTEKET ===\n");
            foreach (var book in books)
            {
                Console.WriteLine(book.GetInfo());
            }
            Console.WriteLine($"\nTotalt: {books.Count} böcker");
        }

        public void ShowStatistics()
        {
            Console.WriteLine("\n=== BIBLIOTEKSSTATISTIK ===\n");
            Console.WriteLine($"Totalt antal böcker: {GetTotalBookCount()}");
            Console.WriteLine($"Antal utlånade böcker: {GetBorrowedItemCount()}");
            Console.WriteLine($"Antal tillgängliga böcker: {GetAvailableItemCount()}");

            var mostActive = GetMostActiveBorrower();
            if (mostActive != null)
            {
                var stats = GetBorrowerStatistics();
                Console.WriteLine($"\nMest aktiv låntagare: {mostActive} ({stats[mostActive]} lån)");

                Console.WriteLine("\nAlla låntagare:");
                foreach (var borrower in stats.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"  - {borrower.Key}: {borrower.Value} lån");
                }
            }
            else
            {
                Console.WriteLine("\nInga aktiva låntagare för tillfället.");
            }
        }
    }
}