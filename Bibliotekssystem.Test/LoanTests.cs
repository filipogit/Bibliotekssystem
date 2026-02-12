using Xunit;
using Bibliotekssystem.Models;

namespace BiblioteksTest
{
    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenDueDateIsInFuture()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com", DateTime.Now.AddYears(-1));
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act & Assert
            Assert.False(loan.IsOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenDueDateHasPassed()
        {
            // Arrange
            var book = new Book("123", "Försenad bok", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com", DateTime.Now.AddYears(-1));
            var loan = new Loan(book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-5));

            // Act
            var isOverdue = loan.IsOverdue;

            // Assert
            Assert.True(isOverdue);
        }

        [Fact]
        public void IsReturned_ShouldReturnTrue_WhenReturnDateIsSet()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com", DateTime.Now.AddYears(-1));
            var loan = new Loan(book, member, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(4));

            // Act
            loan.ReturnBook();

            // Assert
            Assert.True(loan.IsReturned);
            Assert.NotNull(loan.ReturnDate);
        }
    }
}