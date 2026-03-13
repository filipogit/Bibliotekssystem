using System;
using System.ComponentModel.DataAnnotations;
using Bibliotekssystem.Interfaces;

namespace Bibliotekssystem.Models
{
    public class Book : LibraryItem
    {
        [Required(ErrorMessage = "ISBN är obligatoriskt.")]
        [StringLength(20, ErrorMessage = "ISBN får max vara 20 tecken.")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Författare är obligatoriskt.")]
        [StringLength(100, ErrorMessage = "Författarnamn får max vara 100 tecken.")]
        public string Author { get; set; } = string.Empty;
    
        // Navigation properties
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public Book() { }

        public Book(string isbn, string title, string author, int publishedYear)
            : base(title, publishedYear)
        {
            ISBN = isbn;
            Author = author;
        }

        // Override Matches för att inkludera ISBN och Author
        public override bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;

            searchTerm = searchTerm.ToLower();

            return base.Matches(searchTerm) ||
                   ISBN.ToLower().Contains(searchTerm) ||
                   Author.ToLower().Contains(searchTerm);
        }


        public override string GetInfo()
        {
            string status = IsAvailable ? "Tillgänglig" : $"Utlånad till {BorrowedBy}";
            return $"BOK - ISBN: {ISBN}, Titel: {Title}, Författare: {Author}, Utgiven: {PublishedYear}, Status: {status}";
        }
    }
}

/*
Properties: ISBN(string), Title(string), Author(string), PublishedYear(int), IsAvailable(bool)
Konstruktor som tar obligatoriska parametrar
ISBN ska endast kunna sättas vid skapande
Metod GetInfo() som returnerar formaterad bokinformation
*/
