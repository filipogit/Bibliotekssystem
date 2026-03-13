using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using Bibliotekssystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BiblioteksTest
{
    public class BookRepositoryTests
    {
        // Varje test får sin egen isolerade InMemory-databas via ett unikt namn
        private static LibraryContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveBookToDatabase()
        {
            // Arrange
            using var context = CreateContext(nameof(AddAsync_ShouldSaveBookToDatabase));
            var repository = new BookRepository(context);
            var book = new Book("123", "Test", "Author", 2024);

            // Act
            await repository.AddAsync(book);

            // Assert
            var savedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123");
            Assert.NotNull(savedBook);
            Assert.Equal("Test", savedBook.Title);
            Assert.Equal("Author", savedBook.Author);
            Assert.Equal(2024, savedBook.PublishedYear);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBooks()
        {
            // Arrange
            using var context = CreateContext(nameof(GetAllAsync_ShouldReturnAllBooks));
            var repository = new BookRepository(context);

            context.Books.AddRange(
                new Book("111", "Bok ett", "Författare A", 2020),
                new Book("222", "Bok två", "Författare B", 2021),
                new Book("333", "Bok tre", "Författare C", 2022)
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoBooksExist()
        {
            // Arrange
            using var context = CreateContext(nameof(GetAllAsync_ShouldReturnEmptyList_WhenNoBooksExist));
            var repository = new BookRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task SearchAsync_ShouldFindBooksByTitle()
        {
            // Arrange
            using var context = CreateContext(nameof(SearchAsync_ShouldFindBooksByTitle));
            var repository = new BookRepository(context);

            context.Books.AddRange(
                new Book("111", "Sagan om ringen", "J.R.R. Tolkien", 1954),
                new Book("222", "Harry Potter", "J.K. Rowling", 1997),
                new Book("333", "Sagan om Narnia", "C.S. Lewis", 1950)
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.SearchAsync("Sagan");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, b => Assert.Contains("Sagan", b.Title));
        }

        [Fact]
        public async Task SearchAsync_ShouldFindBooksByAuthor()
        {
            // Arrange
            using var context = CreateContext(nameof(SearchAsync_ShouldFindBooksByAuthor));
            var repository = new BookRepository(context);

            context.Books.AddRange(
                new Book("111", "Sagan om ringen", "J.R.R. Tolkien", 1954),
                new Book("222", "Hobbit", "J.R.R. Tolkien", 1937),
                new Book("333", "Harry Potter", "J.K. Rowling", 1997)
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.SearchAsync("Tolkien");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, b => Assert.Equal("J.R.R. Tolkien", b.Author));
        }

        [Fact]
        public async Task SearchAsync_ShouldFindBooksByISBN()
        {
            // Arrange
            using var context = CreateContext(nameof(SearchAsync_ShouldFindBooksByISBN));
            var repository = new BookRepository(context);

            context.Books.AddRange(
                new Book("978-91-001", "Bok A", "Författare A", 2020),
                new Book("978-91-002", "Bok B", "Författare B", 2021)
            );
            await context.SaveChangesAsync();

            // Act
            var result = await repository.SearchAsync("978-91-001");

            // Assert
            Assert.Single(result);
            Assert.Equal("Bok A", result[0].Title);
        }

        [Fact]
        public async Task SearchAsync_ShouldBeCaseInsensitive()
        {
            // Arrange
            using var context = CreateContext(nameof(SearchAsync_ShouldBeCaseInsensitive));
            var repository = new BookRepository(context);

            context.Books.Add(new Book("111", "Sagan om ringen", "J.R.R. Tolkien", 1954));
            await context.SaveChangesAsync();

            // Act
            var lowerResult = await repository.SearchAsync("tolkien");
            var upperResult = await repository.SearchAsync("TOLKIEN");

            // Assert
            Assert.Single(lowerResult);
            Assert.Single(upperResult);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnEmpty_WhenNoMatchFound()
        {
            // Arrange
            using var context = CreateContext(nameof(SearchAsync_ShouldReturnEmpty_WhenNoMatchFound));
            var repository = new BookRepository(context);

            context.Books.Add(new Book("111", "Sagan om ringen", "J.R.R. Tolkien", 1954));
            await context.SaveChangesAsync();

            // Act
            var result = await repository.SearchAsync("Astrid Lindgren");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddAsync_ShouldSetIsAvailableToTrue_ByDefault()
        {
            // Arrange
            using var context = CreateContext(nameof(AddAsync_ShouldSetIsAvailableToTrue_ByDefault));
            var repository = new BookRepository(context);
            var book = new Book("555", "Ny bok", "Ny författare", 2025);

            // Act
            await repository.AddAsync(book);

            // Assert
            var savedBook = await context.Books.FirstAsync(b => b.ISBN == "555");
            Assert.True(savedBook.IsAvailable);
        }
    }
}
