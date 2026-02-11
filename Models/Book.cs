using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotekssystem.Models
{
    public class Book
    {
        public Book(string iSBN, string title, string author, int publishedYear)
        {
            ISBN = iSBN;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
        }

        public string ISBN { get; init; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }

        public string GetInfo()
        {
            return $"ISBN: {ISBN}, Title: {Title}, Author: {Author}, Published Year: {PublishedYear}, Available: {IsAvailable}";
        }
    }
}

/* Properties: ISBN(string), Title(string), Author(string), PublishedYear(int), IsAvailable(bool)
Konstruktor som tar obligatoriska parametrar
ISBN ska endast kunna sättas vid skapande
Metod GetInfo() som returnerar formaterad bokinformation
*/
