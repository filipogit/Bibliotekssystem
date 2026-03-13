using Xunit;
using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using Bibliotekssystem.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BiblioteksTest
{
    public class LibraryStatisticsTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly LibraryContext _context;
        private readonly Library _library;

        public LibraryStatisticsTests()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new LibraryContext(options);
            _context.Database.EnsureCreated();
            _library = new Library(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
            _connection.Dispose();
        }

        [Fact]
        public void GetTotalBookCount_ShouldReturnCorrectCount()
        {
            // Arrange
            _library.AddBook(new Book("123", "Bok 1", "Författare 1", 2020));
            _library.AddBook(new Book("456", "Bok 2", "Författare 2", 2021));

            // Act
            var totalBooks = _library.GetTotalBookCount();

            // Assert
            Assert.Equal(2, totalBooks);
        }

        [Fact]
        public void GetBorrowedItemCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var book1 = new Book("123", "Bok 1", "Författare 1", 2020);
            var book2 = new Book("456", "Bok 2", "Författare 2", 2021);
            var book3 = new Book("789", "Bok 3", "Författare 3", 2022);

            _library.AddBook(book1);
            _library.AddBook(book2);
            _library.AddBook(book3);

            // Låna ut två böcker
            book1.IsAvailable = false;
            book2.IsAvailable = false;
            _context.SaveChanges();

            // Act
            var borrowedCount = _library.GetBorrowedItemCount();

            // Assert
            Assert.Equal(2, borrowedCount);
        }

        [Fact]
        public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
        {
            // Arrange
            var book1 = new Book("123", "Bok 1", "Författare 1", 2020);
            var book2 = new Book("456", "Bok 2", "Författare 2", 2021);
            var book3 = new Book("789", "Bok 3", "Författare 3", 2022);
            var anna = new Member("Anna Andersson", "anna@test.com");
            var erik = new Member("Erik Eriksson", "erik@test.com");

            _library.AddBook(book1);
            _library.AddBook(book2);
            _library.AddBook(book3);
            _library.AddMember(anna);
            _library.AddMember(erik);

            // Anna lånar 2 böcker, Erik lånar 1
            _context.Loans.Add(new Loan(book1, anna, DateTime.Now, DateTime.Now.AddDays(14)));
            _context.Loans.Add(new Loan(book2, anna, DateTime.Now, DateTime.Now.AddDays(14)));
            _context.Loans.Add(new Loan(book3, erik, DateTime.Now, DateTime.Now.AddDays(14)));
            _context.SaveChanges();

            // Act
            var mostActive = _library.GetMostActiveBorrower();

            // Assert
            Assert.Equal("Anna Andersson", mostActive);
        }

        [Fact]
        public void SortByTitle_ShouldReturnAlphabeticalOrder()
        {
            // Arrange
            _library.AddBook(new Book("123", "Zebra", "Författare 1", 2020));
            _library.AddBook(new Book("456", "Apa", "Författare 2", 2021));
            _library.AddBook(new Book("789", "Björn", "Författare 3", 2022));

            // Act
            var sortedBooks = _library.SortByTitle(ascending: true);

            // Assert
            Assert.Equal("Apa", sortedBooks[0].Title);
            Assert.Equal("Björn", sortedBooks[1].Title);
            Assert.Equal("Zebra", sortedBooks[2].Title);
        }
    }
}