using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class Book : LibraryItem
    {
        public string ISBN { get; init; }
        public string Author { get; set; }

        public Book(string isbn, string title, string author, int publishedYear)
            : base(isbn, title, publishedYear) // ISBN används som Id
        {
            ISBN = isbn;

            Author = author;

        }

        // Override GetInfo för att inkludera bokspecifika detaljer
        public override string GetInfo()
        {
            return $"BOK - ISBN: {ISBN}, Titel: {Title}, Författare: {Author}, Utgiven: {PublishedYear}, Tillgänglig: {IsAvailable}";
        }
    }
}

/*
Properties: ISBN(string), Title(string), Author(string), PublishedYear(int), IsAvailable(bool)
Konstruktor som tar obligatoriska parametrar
ISBN ska endast kunna sättas vid skapande
Metod GetInfo() som returnerar formaterad bokinformation
*/
