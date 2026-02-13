using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bibliotekssystem.Interfaces;

namespace Bibliotekssystem.Models
{
    public class LibraryItem : ISearchable
    {
        public string Id { get; init; }
        public string Title { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
        public string? BorrowedBy { get; set; } // Ny egenskap för statistik

        public LibraryItem(string id, string title, int publishedYear)
        {
            Id = id;
            Title = title;
            PublishedYear = publishedYear;
            IsAvailable = true;
            BorrowedBy = null;
        }

        public virtual void CheckOut(string borrowerName)
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                BorrowedBy = borrowerName;
                Console.WriteLine($"{Title} har lånats ut till {borrowerName}.");
            }
            else
            {
                Console.WriteLine($"{Title} är redan utlånad till {BorrowedBy}.");
            }
        }

        public virtual void Return()
        {
            if (!IsAvailable)
            {
                Console.WriteLine($"{Title} har returnerats av {BorrowedBy}.");
                IsAvailable = true;
                BorrowedBy = null;
            }
            else
            {
                Console.WriteLine($"{Title} är redan tillgänglig.");
            }
        }

        // Implementering av ISearchable
        public virtual bool Matches(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return false;

            searchTerm = searchTerm.ToLower();

            return Title.ToLower().Contains(searchTerm) ||
                   Id.ToLower().Contains(searchTerm) ||
                   PublishedYear.ToString().Contains(searchTerm);
        }

        public virtual string GetInfo()
        {
            string status = IsAvailable ? "Tillgänglig" : $"Utlånad till {BorrowedBy}";
            return $"ID: {Id}, Titel: {Title}, Utgiven: {PublishedYear}, Status: {status}";
        }
    }
}