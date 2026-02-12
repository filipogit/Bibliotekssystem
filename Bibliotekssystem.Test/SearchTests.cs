using Xunit;
using Bibliotekssystem.Models;

namespace BiblioteksTest
{
    public class SearchTests
    {
        [Theory]
        [InlineData("Tolkien", true)]
        [InlineData("tolkien", true)]  // Case-insensitive
        [InlineData("Rowling", false)]
        [InlineData("TOLKIEN", true)]  // Case-insensitive
        public void Book_Matches_ShouldFindByAuthor(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Sagan", true)]      // Söker på titel
        [InlineData("123", true)]        // Söker på ISBN
        [InlineData("1954", true)]       // Söker på år
        [InlineData("Hobbit", false)]    // Finns inte i boken
        public void Book_Matches_ShouldSearchMultipleFields(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}