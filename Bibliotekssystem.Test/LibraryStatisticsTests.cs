using Xunit;
using Bibliotekssystem.Models;
using Bibliotekssystem.Services;

namespace BiblioteksTest
{
    public class LibraryStatisticsTests
    {
        [Fact]
        public void GetTotalBookCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var library = new Library();
            library.AddItem(new Book("123", "Bok 1", "Författare 1", 2020));
            library.AddItem(new Book("456", "Bok 2", "Författare 2", 2021));
            library.AddItem(new Magazine("M001", "Tidning", 2024, 1, "Förlag", "Jan"));
            library.AddItem(new DVD("D001", "Film", 2022, "Regissör", 120, "Action"));

            // Act
            var totalBooks = library.GetTotalBookCount();

            // Assert
            Assert.Equal(2, totalBooks); // Endast böcker, inte tidningar eller DVD
        }

        [Fact]
        public void GetBorrowedItemCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var library = new Library();
            var book1 = new Book("123", "Bok 1", "Författare 1", 2020);
            var book2 = new Book("456", "Bok 2", "Författare 2", 2021);
            var book3 = new Book("789", "Bok 3", "Författare 3", 2022);

            library.AddItem(book1);
            library.AddItem(book2);
            library.AddItem(book3);

            // Låna ut två böcker
            book1.CheckOut("Anna Andersson");
            book2.CheckOut("Erik Eriksson");

            // Act
            var borrowedCount = library.GetBorrowedItemCount();

            // Assert
            Assert.Equal(2, borrowedCount);
        }

        [Fact]
        public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
        {
            // Arrange
            var library = new Library();
            var book1 = new Book("123", "Bok 1", "Författare 1", 2020);
            var book2 = new Book("456", "Bok 2", "Författare 2", 2021);
            var book3 = new Book("789", "Bok 3", "Författare 3", 2022);

            library.AddItem(book1);
            library.AddItem(book2);
            library.AddItem(book3);

            // Anna lånar 2 böcker, Erik lånar 1
            book1.CheckOut("Anna Andersson");
            book2.CheckOut("Anna Andersson");
            book3.CheckOut("Erik Eriksson");

            // Act
            var mostActive = library.GetMostActiveBorrower();

            // Assert
            Assert.Equal("Anna Andersson", mostActive);
        }

        [Fact]
        public void SortByTitle_ShouldReturnAlphabeticalOrder()
        {
            // Arrange
            var library = new Library();
            library.AddItem(new Book("123", "Zebra", "Författare 1", 2020));
            library.AddItem(new Book("456", "Apa", "Författare 2", 2021));
            library.AddItem(new Book("789", "Björn", "Författare 3", 2022));

            // Act
            var sortedBooks = library.SortByTitle(ascending: true);

            // Assert
            Assert.Equal("Apa", sortedBooks[0].Title);
            Assert.Equal("Björn", sortedBooks[1].Title);
            Assert.Equal("Zebra", sortedBooks[2].Title);
        }
    }
}