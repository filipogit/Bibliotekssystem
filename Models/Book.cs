using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bibliotekssystem.Interfaces;

namespace Bibliotekssystem.Models
{
    public class Book : LibraryItem
    {
        public string ISBN { get; set; } = string.Empty;
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
